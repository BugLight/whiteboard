using Microsoft.AspNetCore.Mvc;
using System;
using Whiteboard.Models;

namespace Whiteboard.Controllers
{
    [Route("api/rooms")]
    public class RoomController : Controller
    {
        private readonly AppContext context;

        public RoomController(AppContext context)
        {
            this.context = context;
        }

        // Function for adding room
        [HttpPost("meals")]
        public ActionResult<Room> AddMeal([FromBody] Room room)
        {
            if (room == null)
                return BadRequest();

            if (string.IsNullOrEmpty(room.Name))
                return BadRequest();

            if (room.MaxConnections > 0)
                return BadRequest();

            context.Rooms.Add(room);
            context.SaveChanges();

            return room;
        }
    }
}
