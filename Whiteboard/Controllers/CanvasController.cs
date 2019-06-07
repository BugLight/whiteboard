using Microsoft.AspNetCore.Mvc;
using System;

namespace Whiteboard.Controllers
{
    [Route("api/rooms/{id}/canvas")]
    public class CanvasController : Controller
    {
        private readonly IActiveRoomStorage storage;

        public CanvasController(IActiveRoomStorage storage)
        {
            this.storage = storage;
        }

        [HttpGet]
        public ActionResult Get(Guid id)
        {
            var canvas = storage.GetById(id)?.Canvas;
            if (canvas == null)
                return NotFound();
            return File(canvas.GetBytes(), "image/png");
        }
    }
}
