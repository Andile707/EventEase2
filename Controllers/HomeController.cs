using System.Diagnostics;
using Eventease.Data;
using Eventease.Models;
using Microsoft.AspNetCore.Mvc;

namespace Eventease.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BookingDbContext _context;


        public HomeController(ILogger<HomeController> logger, BookingDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Venue()
        {
            return View();
        }

        public IActionResult DisplayBookings()
        {
            //get all the bookings from the database and pass them to the view
            var Allbookings = _context.Booking.ToList();
            return View(Allbookings);
        }
        public IActionResult Booking(int? BookingId)
        {
            if(BookingId != null)
            {
                //find the booking in the database using the booking id
                var bookingInDb = _context.Booking.SingleOrDefault(b => b.BookingId == BookingId);
                //pass the booking to the view
                return View(bookingInDb);
            }
            return View();
        }

        public IActionResult DeleteBooking(int BookingId)
        {
                //find the booking in the database using the booking id
                var bookingInDb = _context.Booking.SingleOrDefault(b => b.BookingId == BookingId);
            //remove the booking from the database
            if (bookingInDb != null)
            {
                _context.Booking.Remove(bookingInDb);
                //save changes to the database
                _context.SaveChanges();
            }
            return RedirectToAction("Booking");

        }

        public IActionResult BookingForm(BookingManager model)
        {    
            if(model.BookingId == 0)
            {
                //if the booking id is 0, it means that we are creating a new booking
                //so we add the booking to the database
                _context.Booking.Add(model);
            }
            else
            {
                //if the booking id is not 0, it means that we are updating an existing booking
                
                _context.Booking.Update(model);
              
             
            }
            //add the booking to the database
            _context.Booking.Add(model);
          
            //save changes to the database
            _context.SaveChanges();
            //redirect to the booking page after saving the booking
            return RedirectToAction("DisplayBookings");
        }

        public IActionResult Event()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
