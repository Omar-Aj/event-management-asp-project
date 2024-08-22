using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace event_management_asp_project.Models.ViewModels
{
    public class EventViewModel
    {
        //Search Attribute
        [MaxLength(30)]
        public string? Title { get; set; }

        //Search Results
        public IEnumerable<Event>? Events { get; set; }
    }
}
