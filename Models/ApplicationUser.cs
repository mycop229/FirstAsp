using Microsoft.AspNetCore.Identity;

namespace Tor.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }

    }
}
