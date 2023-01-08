using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using Microsoft.AspNetCore.Identity;

namespace NowEvent.Models
{
    public class User : IdentityUser
    {
        [MaxLength(25)]
        [Required]
        public string? FirstName { get; set; }
        [MaxLength(25)]
        [Required]
        public string? LastName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}