using Eventease.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Eventease.Controllers
{
    public class VenueController : Controller
    {
        private readonly EventEaseDbContext _context;
        public VenueController(EventEaseDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string searchTerm)
        {
            if (!string.IsNullOrEmpty(searchTerm))
            {
                var venues = _context.Venues
                    .Where(v => v.venueName.Contains(searchTerm))
                    .ToList();
                return View(venues);
            }
            return View();
        }
    }
}
