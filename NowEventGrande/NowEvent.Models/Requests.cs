namespace NowEvent.Models
{
    public class Request
    {
        public int Id { get; set; }
        public string? ClientId { get; set; }
        public int ContractorId { get; set; }
        public int EventId { get; set; }
        public string Message { get; set; }
        public string ContractorEmail { get; set; }
        public string CompanyName { get; set; }
    }
}
