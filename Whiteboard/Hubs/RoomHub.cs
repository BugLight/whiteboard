using Microsoft.AspNetCore.SignalR;
using System;
using System.Drawing;
using System.Threading.Tasks;

namespace Whiteboard.Hubs
{
    public interface IRoomClient
    {
        Task UserJoined(ActiveRoom room);
        Task UserLeft(ActiveRoom room);
        Task Drew(Movement m);
    }

    public class RoomHub : Hub<IRoomClient>
    {
        private readonly IConnectionStorage connectionStorage;
        private readonly IActiveRoomStorage activeRoomStorage;

        public RoomHub(IConnectionStorage connectionStorage, IActiveRoomStorage activeRoomStorage)
        {
            this.connectionStorage = connectionStorage;
            this.activeRoomStorage = activeRoomStorage;
        }

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync().ConfigureAwait(false);
            var connection = connectionStorage.GetById(Context.ConnectionId);
            if (connection == null)
            {
                connection = new Connection(Context.ConnectionId);
                connectionStorage.Add(Context.ConnectionId, connection);
            }
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await base.OnDisconnectedAsync(exception).ConfigureAwait(false);
            var connection = connectionStorage.GetById(Context.ConnectionId);
            if (connection != null)
            {
                var room = connection.Room;
                connection.Close();
                if (room != null)
                {
                    var id = room.Id.ToString();
                    await Groups.RemoveFromGroupAsync(Context.ConnectionId, id).ConfigureAwait(false);
                    await Clients.Group(id).UserLeft(room).ConfigureAwait(false);
                }
            }
        }

        public async Task UserJoin(Guid id)
        {
            var connection = connectionStorage.GetById(Context.ConnectionId);
            if (connection.Room != null)
            {
                connection.Room.Leave(connection);
            }
            var room = activeRoomStorage.GetById(id);
            if (room != null)
            {
                room.Join(connection);
                await Groups.AddToGroupAsync(Context.ConnectionId, id.ToString()).ConfigureAwait(false);
                await Clients.Group(id.ToString()).UserJoined(connection.Room).ConfigureAwait(false);
            }
        }

        public async Task UserLeave()
        {
            var connection = connectionStorage.GetById(Context.ConnectionId);
            if (connection.Room != null)
            {
                var id = connection.Room.Id.ToString();
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, id).ConfigureAwait(false);
                await Clients.Group(id).UserLeft(connection.Room).ConfigureAwait(false);
                connection.Room.Leave(connection);
            }   
        }

        public async Task Draw(Movement m)
        {
            var connection = connectionStorage.GetById(Context.ConnectionId);
            if (connection.Room == null)
                throw new Exception();
            var id = connection.Room.Id.ToString();
            await Clients.Group(id).Drew(m).ConfigureAwait(false);
            connection.Room.Canvas.DrawLine(Color.Black, m.From, m.To);
        }
    }
}
