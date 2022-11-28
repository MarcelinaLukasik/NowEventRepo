namespace WebApplication2.Services.VerificationService
{
    public interface IVerificationService
    {
        Dictionary<string, string> GetVerificationInfo(int eventId);
        void VerifyPlaceStatus(string placeStatus);
    }
}
