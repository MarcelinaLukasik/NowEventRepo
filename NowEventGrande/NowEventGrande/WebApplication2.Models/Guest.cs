using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace WebApplication2.Models
{
    public class Guest
    {
        public int Id { get; set; }

        [ForeignKey("Event")]
        public int EventId { get; set; }
        public virtual Event? Event { get; set; }


        [MaxLength(25)]
        [Required]
        public string FirstName { get; set; }
        [MaxLength(25)]
        [Required]
        public string LastName { get; set; }
        [MaxLength(50)]
        [Required]
        public string Email { get; set; }

    }
}