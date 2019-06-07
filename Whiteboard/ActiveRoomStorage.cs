using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Concurrent;
using System.Linq;
using Whiteboard.Models;

namespace Whiteboard
{
    public class ActiveRoomStorage : IActiveRoomStorage
    {
        private readonly ConcurrentDictionary<Guid, ActiveRoom> activeRooms = new ConcurrentDictionary<Guid, ActiveRoom>();

        private readonly DbContextOptions contextOptions;

        public ActiveRoomStorage(DbContextOptions options)
        {
            contextOptions = options;
        }

        private AppContext CreateContext()
        {
            return new AppContext(contextOptions);
        }

        public void Add(Guid id, ActiveRoom item)
        {
            activeRooms[id] = item;
            item.OnLeft += (sender, args) =>
            {
                var room = (ActiveRoom)sender;
                if (room.ConnectionsCount == 0)
                {
                    Remove(room.Id);
                    room.Canvas.Flush();
                    using (var context = CreateContext())
                        context.SaveChanges();
                }
            };
        }

        public ActiveRoom GetById(Guid id)
        {
            if (!activeRooms.ContainsKey(id))
            {
                Room room;
                using (var context = CreateContext())
                    room = context.Rooms.Include(r => r.Canvas).FirstOrDefault(r => r.Id == id);
                if (room == null)
                    return null;
                ActiveRoom activeRoom = new ActiveRoom(room);
                Add(room.Id, activeRoom);
                return activeRoom;
            }
            return activeRooms[id];
        }

        public void Remove(Guid id)
        {
            activeRooms.TryRemove(id, out _);
        }
    }
}
