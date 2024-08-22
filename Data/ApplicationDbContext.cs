using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using event_management_asp_project.Models;

namespace event_management_asp_project.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Event> tblEvents { get; set; } = default!;
        public DbSet<Venue> tblVenues { get; set; } = default!;
        public DbSet<Guest> tblGuests { get; set; } = default!;
        public DbSet<Reservation> tblReservations { get; set; } = default!;
    }
}
