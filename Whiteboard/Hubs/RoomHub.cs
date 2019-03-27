using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Whiteboard.Hubs
{
    public interface IRoomClient
    {
        Task UserJoined();
        Task UserLeft();
    }

    public class RoomHub : Hub<IRoomClient>
    {
        public async Task UserJoin(string room)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, room);
            await Clients.Group(room).UserJoined();
        }

        public async Task UserLeave(string room)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, room);
            await Clients.Group(room).UserLeft();
        }
    }
}
