using Eventease.Data;
using Eventease.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Eventease.Controllers
{
    public class VenueController : Controller
    {
        private readonly EventEaseDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public VenueController(
            EventEaseDbContext context,
            IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult Venueview()
        {
            return View("CreateVenue");
        }

        public IActionResult DisplayVenues()
        {
            var AllVenues = _context.Venues.ToList();
            return View(AllVenues);
        }

        [HttpGet]
        public IActionResult Venue()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateVenue(VenueModel model)
        {


          //  if (ModelState.IsValid)
            //{
                _context.Venues.Add(model);

                _context.SaveChanges();
                //return RedirectToAction(nameof(Index));
                return View(model);
           // }


            //return RedirectToAction("Success");
         //   return RedirectToAction("Venueview");

        }
        [HttpGet]
        public IActionResult VenueDetails(int? VenueId)
        {
            if (VenueId != null)
            {
                var venueInDb = _context.Venues
                    .SingleOrDefault(v => v.venueId == VenueId);

                return View(venueInDb);
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddVenueImage(VenueModel venue)
        {
            if (ModelState.IsValid)
            {
                if (venue.venueImage != null)
                {
                    string folder = "images/venues/";
                    folder += Guid.NewGuid().ToString() + "_"
                        + venue.venueImage.FileName;

                    string serverFolder = Path.Combine(
                        _webHostEnvironment.WebRootPath,
                        folder);

                    await venue.venueImage.CopyToAsync(
                        new FileStream(serverFolder, FileMode.Create));
                }

                return RedirectToAction("Booking", "Home");
            }

            return View("_VenueForm");
        }
    }
}
