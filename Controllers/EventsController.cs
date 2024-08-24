using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using event_management_asp_project.Data;
using event_management_asp_project.Models;
using event_management_asp_project.Models.ViewModels;
using System.Security.Claims;

namespace event_management_asp_project.Controllers
{
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult FindEvent()
        {
            return View(new EventViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> FindEvent(EventViewModel model)
        {
            var title = String.IsNullOrEmpty(model.Title) ? "" : model.Title;

            model.Events = await _context.tblEvents
                .Include(e => e.Reservations)!
                .ThenInclude(r => r.Venue)
                .Where(e => e.Title.ToUpper().Contains(title.ToUpper()))
                .ToListAsync();

            return View(model);
        }

        // GET: Events
        public async Task<IActionResult> Index(string sortOrder)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewData["DurationSortParm"] = sortOrder == "Duration" ? "duration_desc" : "Duration";

            var events = from e in _context.tblEvents select e;

            switch (sortOrder)
            {
                case "title_desc":
                    events = events.OrderByDescending(e => e.Title);
                    break;
                case "Duration":
                    events = events.OrderBy(e => e.DurationInHours);
                    break;
                case "duration_desc":
                    events = events.OrderByDescending(e => e.DurationInHours);
                    break;
                default:
                    events = events.OrderBy(e => e.Title);
                    break;
            }

            return View(await events.ToListAsync());
        }

        // GET: Events/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.tblEvents
                .FirstOrDefaultAsync(m => m.EventId == id);
            if (@event == null)
            {
                return NotFound();
            }

            if (@event.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return RedirectToAction("Details", "MyEvents", new { id = id });
            }

            if (ViewBag.GuestModel is null || !ViewBag.GuestModel.ModelState.IsValid)
                ViewBag.GuestModel = new Guest { EventId = id.Value};

            ViewBag.MyGuestModel = null;

            return View(@event);
        }

        // GET: Events/EventExists/5
        private bool EventExists(int id)
        {
            return _context.tblEvents.Any(e => e.EventId == id);
        }
    }
}
