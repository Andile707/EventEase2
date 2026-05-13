using Eventease.Data;
using Eventease.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Eventease.Controllers
{
    public class EventController : Controller
    {
        private readonly ILogger<EventController> _logger;
        private readonly EventEaseDbContext _context;


        public EventController(ILogger<EventController> logger, EventEaseDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult EventPage()
        {
            return View("EventPage");
        }

        public IActionResult EventForm()
        {
            return View("CreateEvent");
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

        

        public IActionResult Eventview()
            { 
               return View("CreateEvent");
        }

       
        public IActionResult DisplayEvents()
        {
            var AllEvents = _context.Events.ToList();
            return View(AllEvents);
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

        [HttpPost]

        public IActionResult CreateEvent(EventModel model)
        {
            

            if (ModelState.IsValid)
                {
                    _context.Events.Add(model);

                    _context.SaveChanges();
                    //return RedirectToAction(nameof(Index));
                    return View(model);
                }
            
                
                //return RedirectToAction("Success");
                return RedirectToAction("Eventview");

        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
