namespace Eventease.Models
{
    public class BookingManager
    {
        public int BookingId { get; set; }
        public required string BookingName { get; set; }
        public int VenueId { get; set; }
        public int EventId {  get; set; }
    }
}
