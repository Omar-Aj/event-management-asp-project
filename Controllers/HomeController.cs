using event_management_asp_project.Data;
using event_management_asp_project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace event_management_asp_project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            //idea: bring first 3 events by popularity
            var events =await _context.tblEvents.Include(e => e.Reservations).OrderByDescending(e => e.Reservations!.Max(r => r.ReservationDate)).Take(3).ToListAsync();
            return View(events);
        }

        [Route("[Action]")]
        public IActionResult About()
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
