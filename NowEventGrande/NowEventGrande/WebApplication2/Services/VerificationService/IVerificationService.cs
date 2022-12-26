using WebApplication2.Models;

namespace WebApplication2.Services.VerificationService
{
    public interface IVerificationService
    {
        Dictionary<string, string> GetVerificationInfo(int eventId);
        void VerifyPlaceStatus(string placeStatus);
        void VerifyPlaceHours(string allHours, int id);
        bool VerifyGuest(Guest guest);
        bool VerifyGuestName(Guest guest);
        bool VerifyEvent(Event newEvent);
        bool CheckBudgetFullStatus(int eventId);
        bool VerifyBudgetPrice(string budgetPrice);
    }
}
