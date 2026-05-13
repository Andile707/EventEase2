using Eventease.Models;
using Microsoft.EntityFrameworkCore;

namespace Eventease.Data
{
    public class EventEaseDbContext : DbContext
    {
        public DbSet<BookingModel> Bookings { get; set; }
        public DbSet<EventModel> Events { get; set; }
        public DbSet<VenueModel> Venues { get; set; }
        public EventEaseDbContext(DbContextOptions<EventEaseDbContext> options) : base(options)
        {
        }
       
      
    }
}
