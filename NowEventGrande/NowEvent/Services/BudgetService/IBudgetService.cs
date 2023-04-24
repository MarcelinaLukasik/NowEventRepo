using NowEvent.Models;
using NowEvent.Models.Constants;

namespace NowEvent.Services.BudgetService
{
    public interface IBudgetService
    {
        Task ChangePrice(decimal price, int eventId, BudgetOptions budgetOption);
        Task<Budget> GetBudget(int eventId);
        Dictionary<BudgetOptions, decimal> GetAllPrices(int eventId);
    }
}
