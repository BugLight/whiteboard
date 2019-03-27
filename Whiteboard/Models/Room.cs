using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Whiteboard.Models
{
    public class Room
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]

        // Primary key
        public Guid Id { get; set; }

        // Name of room
        public string Name { get; set; }
        // Number of maximum amount of connections
        public int MaxConnections { get; set; }
    }
}
