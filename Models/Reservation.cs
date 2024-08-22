using System.ComponentModel.DataAnnotations;

namespace event_management_asp_project.Models
{
    public class Reservation
    {
        public int ReservationId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime ReservationDate { get; set; }

        //Foreign Keys
        public int EventId { get; set; }
        public int VenueId { get; set; }

        //Navigation
        public Event? Event { get; set; }
        public Venue? Venue { get; set; }

    }
}
