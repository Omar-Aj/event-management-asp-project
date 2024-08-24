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
using Microsoft.AspNetCore.Authorization;

namespace event_management_asp_project.Controllers
{
    [Authorize]
    public class MyEventsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public MyEventsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MyEvents
        public async Task<IActionResult> Index()
        {
            var model = await _context.tblEvents.Include(x => x.Reservations).Where(x => x.UserId.Equals(User.FindFirstValue(ClaimTypes.NameIdentifier))).ToListAsync();
            return View(model);
        }

        // GET: MyEvents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.tblEvents
                .Include(e => e.Reservations)
                .FirstOrDefaultAsync(m => m.EventId == id);

            if (@event == null)
            {
                return NotFound();
            }

            if (@event.UserId != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return Unauthorized();
            }

            ViewData["Venues"] = new SelectList(await _context.tblVenues.ToListAsync(), "VenueId", "Name");

            return View(@event);
        }

        // GET: MyEvents/Create
        public IActionResult Create()
        {
            return View(new Event { UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)! });
        }

        // POST: MyEvents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventId,Title,DurationInHours,Description,UserId")] Event @event)
        {
            if (@event.UserId != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return Unauthorized();
            }

            if (ModelState.IsValid)
            {
                _context.Add(@event);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { id = @event.EventId });
            }

            return View(@event);
        }

        // GET: MyEvents/Edit/5
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

            if (@event.UserId != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return Unauthorized();
            }

            return View(@event);
        }

        // POST: MyEvents/Edit/5
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

            if (@event.UserId != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return Unauthorized();
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
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }

            return View(@event);
        }

        // GET: MyEvents/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

            if (@event.UserId != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return Unauthorized();
            }

            return View(@event);
        }

        // POST: MyEvents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @event = await _context.tblEvents.FindAsync(id);

            if (@event != null && @event.UserId != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return Unauthorized();
            }

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
