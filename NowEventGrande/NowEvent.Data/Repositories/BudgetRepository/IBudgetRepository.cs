using NowEvent.Models;
using NowEvent.Models.Constants;

namespace NowEvent.Data
{
    public interface IBudgetRepository
    {
        void AddBudget(Budget budget);
        Task ChangePrice(decimal price, int eventId, BudgetOptions budgetOption);
        Dictionary<BudgetOptions, decimal> GetAllPrices(int eventId);
        Task<Budget> GetBudget(int eventId);
        Budget CreateBudget(int eventId);

    }
}
