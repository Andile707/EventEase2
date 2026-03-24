using Eventease.Models;
using Microsoft.EntityFrameworkCore;

namespace Eventease.Data
{
    public class BookingDbContext : DbContext
    {
        public BookingDbContext(DbContextOptions<BookingDbContext> options) : base(options)
        {
        }
        public DbSet<BookingManager> Booking { get; set; }
    }
}
