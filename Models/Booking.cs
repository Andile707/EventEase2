using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;



namespace Eventease.Models
{
    public class Booking
    {
        
        // This class represents a booking in the system. It contains properties for the booking ID, name, venue ID, event ID, and booking date.
        [Key]
        public int BookingId { get; set; }

        [Required(ErrorMessage = "Booking name is required")]

        public string BookingName { get; set; } = "";

        
        [Required]
        public int VenueId { get; set; } 

        
        [Required]
        public int EventId { get; set; } 


        [Required]
        public DateOnly BookingDate { get; set; } 

        
        
    }
}
