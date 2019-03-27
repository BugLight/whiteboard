using Microsoft.EntityFrameworkCore;
using System;
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
