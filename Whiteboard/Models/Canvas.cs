using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
    }
}
