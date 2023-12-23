using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASM2_AppDev.Models
{
    public class ApplicationUser : IdentityUser
    {

        public string? FullName { get; set; }
        public string? Address { get; set; }

        public string? City { get; set; }
        [NotMapped]
        public string Role { get; set; }

    }
}

