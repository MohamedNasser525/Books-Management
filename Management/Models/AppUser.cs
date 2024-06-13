using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Versioning;

namespace Management.Models
{
    public class AppUser : IdentityUser
    {
        [Required,MaxLength(100)]
        public string Fname { get; set; }


        [Required, MaxLength(100)]
        public string Lname { get; set; }

        public byte[] Profileimg { get; set; } = { 10, 20, 30, 40, 50 };

        //public List<JwtRefreshToken>? RefreshTokens { get; set; }
    }
}
