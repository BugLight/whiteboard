using Microsoft.EntityFrameworkCore;
using System;
using Whiteboard.Models;

namespace Whiteboard
{
    public class AppContext : DbContext
    {
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Canvas> Canvases { get; set; }

        public AppContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Canvas>().Property("Content");
        }
    }
}
