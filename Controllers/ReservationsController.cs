using System;
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
    public class ReservationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReservationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReservationId,ReservationDate,VenueId,EventId")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reservation);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "MyEvents", new {id = reservation.EventId});
            }
            return RedirectToAction("Index", "MyEvents");
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed([Bind("ReservationId,ReservationDate,VenueId,EventId")] Reservation model)
        {
            int? eventId = model.EventId;
            if (ModelState.IsValid)
            {
                _context.tblReservations.Remove(model);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "MyEvents", new { id = eventId });
        }

        private bool ReservationExists(int id)
        {
            return _context.tblReservations.Any(e => e.ReservationId == id);
        }
    }
}
