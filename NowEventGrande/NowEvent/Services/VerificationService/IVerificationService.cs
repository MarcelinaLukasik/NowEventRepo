using NowEvent.Models;

namespace NowEvent.Services.VerificationService
{
    public interface IVerificationService
    {
        Dictionary<string, string> GetVerificationInfo(int eventId);
        bool VerifyGuest(Guest guest);
        bool VerifyEvent(Event newEvent);
        bool CheckBudgetFullStatus(int eventId);
        bool VerifyBudgetPrice(string budgetPrice);
        bool VerifyTheme(string theme);
    }
}
