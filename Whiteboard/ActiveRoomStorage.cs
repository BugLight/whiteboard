using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Whiteboard.Models;

namespace Whiteboard
{
    public class ActiveRoomStorage : IActiveRoomStorage
    {
        private static readonly Dictionary<Guid, ActiveRoom> activeRooms = new Dictionary<Guid, ActiveRoom>();

        private readonly AppContext context;

        public ActiveRoomStorage(AppContext context)
        {
            this.context = context;
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
                    context.Add((Room)room);
                    context.SaveChanges();
                }
            };
        }

        public ActiveRoom GetById(Guid id)
        {
            if (!activeRooms.ContainsKey(id))
            {
                Room room = context.Rooms.Include(r => r.Canvas).FirstOrDefault(r => r.Id == id);
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
            activeRooms.Remove(id);
        }
    }
}
