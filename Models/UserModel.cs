using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TH4_Nhom20.Models
{
    public class UserModel : IdentityUser
    {
        [Required]
        public string Name { get; set; }
        public string? Address { get; set; }
        public string? Role { get; set; }
    }
}

