using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Whiteboard.Models
{
    public class Canvas
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        // Primary key
        public Guid Id { get; set; }

        // Id of room
        public Guid RoomId { get; set; }
        // Content of canvas
        public byte[] Content { get; set; }
        // Last time of modification
        public DateTime ModifiedAt { get; set; }

        [NotMapped]
        private Bitmap buffer;

        public void DrawLine(Color color, Point from, Point to)
        {
            if (buffer == null)
            {
                InitBuffer();
            }
            using (var g = Graphics.FromImage(buffer))
            {
                g.DrawLine(new Pen(color), from, to);
                g.Flush();
            }
        }

        private void InitBuffer()
        {
            if (Content == null)
            {
                buffer = new Bitmap(1000, 600);
            }
            else
            {
                buffer = new Bitmap(new MemoryStream(Content));
            }
        }

        public void Flush()
        {
            buffer.Save(new MemoryStream(Content), ImageFormat.Bmp);
        }
    }
}
