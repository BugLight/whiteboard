using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

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
