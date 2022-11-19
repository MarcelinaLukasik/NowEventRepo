using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Models
{
    public class ClientAddress
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string City { get; set; }
        [MaxLength(50)]
        public string Street { get; set; }
        [MaxLength(20)]
        public string PostalCode { get; set; }
        [ForeignKey("Client")]
        public int ClientAddressId { get; set; }
        public virtual Client Client { get; set; }
    }
}