using Microsoft.AspNetCore.Identity;

namespace Tor.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }

        public string Address { get; set; }

        public string City { get; set; }
    }
}
