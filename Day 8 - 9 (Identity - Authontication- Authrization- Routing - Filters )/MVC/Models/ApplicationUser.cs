using Microsoft.AspNetCore.Identity;

namespace MVC.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Faculty { get; set; }

    }
}
