using NowEvent.Models;

namespace NowEvent.Data
{
    public interface IBudgetRepository
    {
        void AddBudget(Budget budget);
        Task ChangePrice(decimal price, int eventId, BudgetPrices budgetPrice);
        Dictionary<BudgetPrices, decimal> GetAllPrices(int eventId);
        Task<Budget> GetBudget(int eventId);
        Budget CreateBudget(int eventId);

    }
}
