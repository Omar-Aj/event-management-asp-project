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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Venue>().HasData(
                new Venue
                {
                    VenueId = 1,
                    Name = "Mecca Mall",
                    Location = "Amman",
                    Capacity = 500,
                    Service = ServiceEnum.Seminar,
                },
                new Venue
                {
                    VenueId = 2,
                    Name = "Mariott Hotel",
                    Location = "Dead Sea",
                    Capacity = 150,
                    Service = ServiceEnum.Wedding,
                },
                new Venue
                {
                    VenueId = 3,
                    Name = "Jerash Festival",
                    Location = "Jerash",
                    Capacity = 900,
                    Service = ServiceEnum.Concert,
                },
                new Venue
                {
                    VenueId = 4,
                    Name = "FINC",
                    Location = "Amman",
                    Capacity = 50,
                    Service = ServiceEnum.Workshop,
                },
                new Venue
                {
                    VenueId = 5,
                    Name = "Kempinski Hotel",
                    Location = "Aqaba",
                    Capacity = 999,
                    Service = ServiceEnum.Meeting,
                }
            );

            base.OnModelCreating(builder);
        }
    }
}
