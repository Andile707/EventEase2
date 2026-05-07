using Eventease.Data;
using Eventease.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Eventease.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly EventEaseDbContext _context;


        public HomeController(ILogger<HomeController> logger, EventEaseDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Index(UploadModel model)
        {
            if (ModelState.IsValid)
            {
                // Process the uploaded file here
                // For example, you can save the file to the server or perform any necessary operations
                // After processing, you can redirect to a success page or return a view with a success message
                return RedirectToAction("Index");
            }
            // If the model state is not valid, return the view with validation errors
            return View(model);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Venue()
        {
            return View();
        }

        public IActionResult Bookingview()
            { 
               return View("_BookingForm");
        }

       /* public IActionResult DisplayBookings()
        {
                //get all the bookings from the database and pass them to the view
               var Allbookings = _context.Bookings;
            _context.SaveChanges();

               if (Allbookings != null)
            {
                return View(Allbookings);
                
            }
            else
            {
                return RedirectToAction("Booking");
            }
        }*/
        public IActionResult DisplayBookings()
        {
            return View();
        }

        public IActionResult Booking(int? BookingId)
        {
            if(BookingId != null) 
            {
                //find the booking in the database using the booking id
                var bookingInDb = _context.Bookings.SingleOrDefault(b => b.BookingId == BookingId);
                //pass the booking to the view
                return View(bookingInDb);
            }
            return View();
        }

        public IActionResult DeleteBooking(int BookingId)
        {
                //find the booking in the database using the booking id
                var bookingInDb = _context.Bookings.SingleOrDefault(b => b.BookingId == BookingId);
            //remove the booking from the database
            if (bookingInDb != null)
            {
                _context.Bookings.Remove(bookingInDb);
                //save changes to the database
                _context.SaveChanges();
            }
            return RedirectToAction("Booking");

        }
       /* public IActionResult AddBooking(int BookingId)
        {
            //find the booking in the database using the booking id
            var bookingInDb = _context.Bookings.SingleOrDefault(b => b.BookingId == BookingId);
            //remove the booking from the database
            if (bookingInDb != null)
            {
                _context.Bookings.Add(bookingInDb);
                //save changes to the database
                _context.SaveChanges();
            }
            return RedirectToAction("Booking");

        }*/

        public IActionResult BookingForm(Booking model)
        {

            if (ModelState.IsValid)
            {
                DateOnly inputDate = (DateOnly)model.BookingDate;

                int inputVenue = model.VenueId;


                bool exists = _context.Bookings
                  .Any(b => b.BookingDate == inputDate && b.VenueId == inputVenue);

                try
                {
                    if (exists)
                    {
                        throw new Exception("Booking date already exists.");
                    }
                }
                catch (Exception ex)
                {
                    // Handle the exception (e.g., log it, display an error message, etc.)
                    // You can also return an error view or redirect to an error page
                    return NotFound(ex.Message);
                }

                if (model.BookingId == 0)
                {
                    //if the booking id is 0, it means that we are creating a new booking
                    //so we add the booking to the database
                    _context.Bookings.Add(model);
                }
                else
                {
                    //if the booking id is not 0, it means that we are updating an existing booking

                    _context.Bookings.Update(model);


                }
                //add the booking to the database
                //_context.Bookings.Add(model);

                //save changes to the database
                _context.SaveChanges();
                return RedirectToAction("DisplayBookings");
            }
            //redirect to the booking page after saving the booking
            return View("_BookingForm");
            
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
