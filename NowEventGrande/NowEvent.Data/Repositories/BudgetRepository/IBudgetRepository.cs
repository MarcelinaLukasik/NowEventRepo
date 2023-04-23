using NowEvent.Models;
using NowEvent.Models.Constants;

namespace NowEvent.Data.Repositories.BudgetRepository
{
    public interface IBudgetRepository
    {
        void AddBudget(Budget budget);
        Task ChangePrice(decimal price, int eventId, BudgetOptions budgetOption);
        decimal GetBudgetPrice(int eventId, BudgetOptions optionType);
        Task<Budget> GetBudget(int eventId);
        Budget CreateBudget(int eventId);

    }
}
