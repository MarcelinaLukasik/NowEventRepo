using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication2.Models;

namespace WebApplication2.Data
{
    public class Event
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int ContractorId { get; set; }
        [MaxLength(15)]
        public string Size { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public DateTime EventStart { get; set; }
        public DateTime EventEnd { get; set; }
        public ICollection<EventAddress>? EventAddresses { get; set; }
        public ICollection<Guest>? Guests { get; set; }
        public ICollection<Budget>? Budgets { get; set; }
        public ICollection<Rating>? Ratings { get; set; }
    }
}