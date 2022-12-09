using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Models
{
    public class Event
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int ContractorId { get; set; }
        [MaxLength(15)]
        public string Size { get; set; }
        public string? SizeRange { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string? Theme { get; set; }
        public string Status { get; set; }
        public DateTime EventStart { get; set; }
        public DateTime EventEnd { get; set; }
        public ICollection<EventAddress>? EventAddresses { get; set; }
        public ICollection<Guest>? Guests { get; set; }
        public ICollection<Budget>? Budgets { get; set; }
        public ICollection<Rating>? Ratings { get; set; }
    }
}