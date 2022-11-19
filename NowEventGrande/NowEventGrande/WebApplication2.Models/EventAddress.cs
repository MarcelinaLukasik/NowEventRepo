using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication2.Data;

namespace WebApplication2.Models
{
    public class EventAddress
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string City { get; set; }
        [MaxLength(50)]
        public string Street { get; set; }
        [MaxLength(20)]
        public string PostalCode { get; set; }
        [ForeignKey("Event")]
        public int EventId { get; set; }
        public virtual Event Event { get; set; }
    }
}