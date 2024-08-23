using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using event_management_asp_project.Data;
using event_management_asp_project.Models;
using System.Security.Claims;
using event_management_asp_project.Models.ViewModels;

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
                .Include(e => e.User)
                .FirstOrDefaultAsync(m => m.EventId == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // GET: Events/Create
        public IActionResult Create()
        {
            return View(new Event { UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)! });
        }

        // POST: Events/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventId,Title,DurationInHours,Description,UserId")] Event @event)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@event);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Set<User>(), "Id", "Id", @event.UserId);
            return View(@event);
        }

        // GET: Events/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.tblEvents.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Set<User>(), "Id", "Id", @event.UserId);
            return View(@event);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EventId,Title,DurationInHours,Description,UserId")] Event @event)
        {
            if (id != @event.EventId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@event);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(@event.EventId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Set<User>(), "Id", "Id", @event.UserId);
            return View(@event);
        }

        // GET: Events/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.tblEvents
                .Include(e => e.User)
                .FirstOrDefaultAsync(m => m.EventId == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @event = await _context.tblEvents.FindAsync(id);
            if (@event != null)
            {
                _context.tblEvents.Remove(@event);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventExists(int id)
        {
            return _context.tblEvents.Any(e => e.EventId == id);
        }
    }
}
