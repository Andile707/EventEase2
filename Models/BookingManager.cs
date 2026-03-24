using System.ComponentModel.DataAnnotations;

namespace Eventease.Models
{
    public class BookingManager
    {
        // This class represents a booking in the system. It contains properties for the booking ID, name, venue ID, event ID, and booking date.
        [Key]
        public int BookingId { get; set; }
        public string? BookingName { get; set; }
        public int VenueId { get; set; }
        public int EventId {  get; set; }
         public DateOnly BookingDate { get; set; }
        
    }
}
