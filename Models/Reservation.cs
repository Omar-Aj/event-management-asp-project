using System.ComponentModel.DataAnnotations;

namespace event_management_asp_project.Models
{
    public class Reservation
    {
        public int ReservationId { get; set; }

        [Required]
        [Display(Name ="Reservation Date & Time")]
        public DateTime ReservationDate { get; set; } = DateTime.Now;

        //Foreign Keys
        public int VenueId { get; set; }
        public int EventId { get; set; }

        //Navigation
        public Venue? Venue { get; set; }
        public Event? Event { get; set; }

    }
}
