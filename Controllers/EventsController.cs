﻿using System;
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

        [Route("~/Search")]
        public IActionResult FindEvent()
        {
            return View(new EventViewModel());
        }

        [Route("~/Search")]
        [HttpPost]
        public async Task<IActionResult> FindEvent(EventViewModel model)
        {
            var title = String.IsNullOrEmpty(model.Title) ? "" : model.Title;

            model.Events = await _context.tblEvents
                .Include(e => e.Reservations)!
                .ThenInclude(r => r.Venue)
                .Where(e => e.Title.ToUpper().Contains(title.ToUpper()))
				.Where(e => e.Reservations!.Count() > 0)
                .ToListAsync();

            return View(model);
        }

        // GET: Events
        public async Task<IActionResult> Index(string sortOrder)
        {
            ViewData["CurrentSort"] = String.IsNullOrEmpty(sortOrder) ? "" : sortOrder;

            IQueryable<Event> events = _context.tblEvents
                            .Include(e => e.Reservations)!
                            .ThenInclude(r => r.Venue)
                            .Where(e => e.Reservations!.Count() > 0);

            switch (sortOrder)
            {
                case "title_desc":
                    events = events.OrderByDescending(e => e.Title);
                    break;
                case "Date":
                    events = events.OrderBy(e => e.Reservations!
                        .Min(r => r.ReservationDate)
                    );
                    break;
                case "date_desc":
                    events = events.OrderByDescending(e => e.Reservations!
                            .Max(r => r.ReservationDate)
                        );
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
        [Route("[Controller]/EventDetails/{id:int}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.tblEvents
                .Include(e => e.Reservations!.OrderByDescending(r => r.ReservationDate))!
				.ThenInclude(r => r.Venue)
				.Include(g => g.Guests)
				.FirstOrDefaultAsync(m => m.EventId == id);

            if (@event == null)
            {
                return NotFound();
            }

			if (@event.Reservations is null || @event.Reservations.Count().Equals(0))
			{
				return NotFound();
			}

			if (@event.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return RedirectToAction("Details", "MyEvents", new { id = id });
            }

            return View(@event);
        }

        // GET: Events/EventExists/5
        private bool EventExists(int id)
        {
            return _context.tblEvents.Any(e => e.EventId == id);
        }
    }
}
