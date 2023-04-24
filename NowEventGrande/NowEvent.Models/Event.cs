using System.ComponentModel.DataAnnotations;

namespace NowEvent.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string? ClientId { get; set; }
        public string? ContractorId { get; set; }
        [MaxLength(15)]
        public string Size { get; set; } = string.Empty;
        public string? SizeRange { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Theme { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime EventStart { get; set; }
        public DateTime EventEnd { get; set; }
        public ICollection<Guest>? Guests { get; set; }
        public ICollection<Rating>? Ratings { get; set; }
    }
}