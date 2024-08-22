using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace event_management_asp_project.Models
{
    public class Venue
    {
        [Key]
        public int VenueId { get; set; }

        [Required]
        [MaxLength(30)]
        [Column(TypeName = "nvarchar(30)")]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Location { get; set; } = string.Empty;

        public ServiceEnum Service { get; set; } = ServiceEnum.Other;

        [Required]
        [Range(5, 1000)]
        public int Capacity { get; set; }

        //Navigation
        public IEnumerable<Reservation>? Reservations { get; set; }

    }
}
