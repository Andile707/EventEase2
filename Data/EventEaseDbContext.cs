using Eventease.Models;
using Microsoft.EntityFrameworkCore;

namespace Eventease.Data
{
    public class EventEaseDbContext : DbContext
    {
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Venue> Venues { get; set; }
        public EventEaseDbContext(DbContextOptions<EventEaseDbContext> options) : base(options)
        {
        }
       
      
    }
}
