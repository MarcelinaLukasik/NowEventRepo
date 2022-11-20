using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Models
{
    public class Budget
    {
        public int Id { get; set; }
        public decimal Total { get; set; }
        public decimal RentPrice { get; set; }
        public decimal FoodPrice { get; set; }
        public decimal DecorationPrice { get; set; }
        [ForeignKey("Event")]
        public int EventId { get; set; }
        public virtual Event? Event { get; set; }
    }
}