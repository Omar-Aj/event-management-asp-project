using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace event_management_asp_project.Models
{
    public class Event
    {
        [Key]
        public int EventId { get; set; }

        [Required]
        [MaxLength(30)]
        [Column(TypeName = "nvarchar(30)")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Duration In Hours")]
        [Range(1, 240)]
        public int DurationInHours { get; set; }

        [Url]
        [Display(Name = "Image URL")]
        public string? ImageUrl { get; set; } = string.Empty;

        [Required]
        [MaxLength(200)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; } = string.Empty;

        //Foreign Key
        public string UserId { get; set; } = string.Empty;

        //Navigation
        public IEnumerable<Reservation>? Reservations { get; set; }
        public IEnumerable<Guest>? Guests { get; set; }
        public User? User { get; set; }
    }
}
