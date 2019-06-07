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
            activeRooms.TryAdd(id, item);
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
            if (!activeRooms.TryGetValue(id, out var activeRoom))
            {
                Room room;
                using (var context = CreateContext())
                    room = context.Rooms.Include(r => r.Canvas).FirstOrDefault(r => r.Id == id);
                if (room == null)
                    return null;
                activeRoom = new ActiveRoom(room);
                Add(room.Id, activeRoom);
            }
            return activeRoom;
        }

        public void Remove(Guid id)
        {
            activeRooms.TryRemove(id, out _);
        }
    }
}
