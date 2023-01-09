using NowEvent.Models;

namespace NowEvent.Services.VerificationService
{
    public interface IVerificationService
    {
        Dictionary<string, string> GetVerificationInfo(int eventId);
        void VerifyPlaceStatus(string placeStatus);
        Task VerifyPlaceHours(string allDaysAndHours, int id);
        bool VerifyGuest(Guest guest);
        bool VerifyGuestName(Guest guest);
        bool VerifyEvent(Event newEvent);
        bool CheckBudgetFullStatus(int eventId);
        bool VerifyBudgetPrice(string budgetPrice);
    }
}
