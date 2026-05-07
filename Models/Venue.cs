namespace Eventease.Models
{
    public class Venue
    {
        public int venueId { get; set; }
        public string venueName { get; set; } = "";
        public string? venueLocation { get; set; }
        public int venueCapacity { get; set; }
        public Uri? venueImageUrl { get; set; }
    }
}
