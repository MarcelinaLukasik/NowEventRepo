using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NowEvent.Models
{
    public class EventAddress
    {
        public int Id { get; set; }
        public string FullAddress { get; set; }
        [Column(TypeName = "decimal(23,9)")]
        public decimal Latitude { get; set; }
        [Column(TypeName = "decimal(23,9)")]
        public decimal Longitude { get; set; }
        public string PlaceId { get; set; }
        public string PlaceOpeningHours { get; set; }
        public string PlaceStatus { get; set; }
        
        [ForeignKey("Event")]
        public int EventId { get; set; }
    }
}