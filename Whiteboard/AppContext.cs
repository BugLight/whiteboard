using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Whiteboard.Models;

namespace Whiteboard
{
    public class AppContext : DbContext
    {
        public DbSet<Room> Rooms { get; set; }

        public AppContext(DbContextOptions options) : base(options)
        {
        }
    }
}
