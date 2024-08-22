using Microsoft.AspNetCore.Identity;

namespace event_management_asp_project.Models
{
    public class User : IdentityUser
    {
        //Navigation
        public IEnumerable<Event>? Events { get; set; }
    }
}
