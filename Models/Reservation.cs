using System.ComponentModel.DataAnnotations;

namespace event_management_asp_project.Models
{
    public class Reservation
    {
        public int ReservationId { get; set; }

        [Required]
        [Display(Name ="Reservation Date & Time")]
        public DateTime ReservationDate { get; set; } = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, 0);

        //Foreign Keys
        public int VenueId { get; set; }
        public int EventId { get; set; }

        //Navigation
        public Venue? Venue { get; set; }
        public Event? Event { get; set; }

    }
}
