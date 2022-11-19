using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace WebApplication2.Models
{
    public class Client
    {
        public int Id { get; set; }
        [MaxLength(25)]
        [Required]
        public string FirstName { get; set; }
        [MaxLength(25)]
        [Required]
        public string LastName { get; set; }
        [MaxLength(50)]
        [Required]
        public string Email { get; set; }
        [MaxLength(25)]
        [Required]
        public string Password { get; set; }
        
        public virtual ClientAddress ClientAddress { get; set; }
    }
}