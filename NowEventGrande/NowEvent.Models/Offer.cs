using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NowEvent.Models
{
    public class Offer
    {
        public int Id { get; set; }
        public string? ClientId { get; set; }
        public int ContractorId { get; set; }
        public int EventId { get; set; }

        public string Status { get; set; }
    }
}
