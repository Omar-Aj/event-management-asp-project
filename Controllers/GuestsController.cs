﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using event_management_asp_project.Data;
using event_management_asp_project.Models;

namespace event_management_asp_project.Controllers
{
    public class GuestsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GuestsController(ApplicationDbContext context)
        {
            _context = context;
        }

		// POST: Guests/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GuestId,EventId,Name,Email,ConfirmEmail,Phone")] Guest guest)
        {
            if (ModelState.IsValid)
            {
                _context.Add(guest);
                await _context.SaveChangesAsync();
				return RedirectToAction("Details", "Events", new { id = guest.EventId });
			}

			return RedirectToAction("Details", "Events", new {id = guest.EventId});
        }

        // POST: Guests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("GuestId,Name,Email,ConfirmEmail,Phone")] Guest guest)
        //{
        //    if (id != guest.GuestId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(guest);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!GuestExists(guest.GuestId))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(guest);
        //}

        // POST: Guests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed([Bind("GuestId,Name,Email,ConfirmEmail,Phone,EventId")] Guest guest)
        {
            int? eventId = guest.EventId;
            if (guest != null)
            {
                _context.tblGuests.Remove(guest);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "MyEvents", new {id = eventId});
        }

        private bool GuestExists(int id)
        {
            return _context.tblGuests.Any(e => e.GuestId == id);
        }
    }
}
