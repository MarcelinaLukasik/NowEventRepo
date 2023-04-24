using System.ComponentModel.DataAnnotations.Schema;

namespace NowEvent.Models
{
    public class EventAddress
    {
        public int Id { get; set; }
        public string FullAddress { get; set; } = string.Empty;
        [Column(TypeName = "decimal(23,9)")]
        public decimal Latitude { get; set; }
        [Column(TypeName = "decimal(23,9)")]
        public decimal Longitude { get; set; }
        public string PlaceId { get; set; } = string.Empty;
        public string PlaceOpeningHours { get; set; } = string.Empty;
        public string PlaceStatus { get; set; } = string.Empty;

        [ForeignKey("Event")]
        public int EventId { get; set; }
    }
}