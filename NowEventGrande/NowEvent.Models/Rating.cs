using System.ComponentModel.DataAnnotations.Schema;

namespace NowEvent.Models
{
    public class Rating
    {
        public int Id { get; set; }

        [ForeignKey("Event")]
        public int EventId { get; set; }

        public int CommunicationRating { get; set; }
        public int QualityRating { get; set; }
    }
}
