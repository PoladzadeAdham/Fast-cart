using Microsoft.AspNetCore.Identity;

namespace fastkart101.Models
{
    public class AppUser : IdentityUser
    {
        public string Fullname { get; set; }
    }
}
