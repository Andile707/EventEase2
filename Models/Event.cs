using System.ComponentModel.DataAnnotations;

namespace Eventease.Models
{
    public class Event
    {
        

        public int EventId { get; set; }

        public int VenueId { get; set; }

        public string? EventName { get; set; }

        public string? EventDescription { get; set; }

         public DateOnly EventDate { get; set; }


    }
}
