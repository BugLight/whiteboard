using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
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
        [HttpPost]
        public ActionResult<Room> AddRoom([FromBody] Room room)
        {
            if (room == null)
                return BadRequest();

            if (string.IsNullOrEmpty(room.Name))
                return BadRequest();

            if (room.MaxConnections <= 0)
                return BadRequest();

            room.Canvas = new Canvas()
            {
                ModifiedAt = DateTime.Now
            };
            context.Rooms.Add(room);
            context.SaveChanges();

            return Ok(room);
        }
    }
}
