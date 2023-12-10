using Microsoft.AspNetCore.Identity;

namespace ASM2_AppDev.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string Birthday { get; set; }
        public string Email { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string Phone { get; set; }
        
    }
}

