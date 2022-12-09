using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.Data
{
    public interface IBudgetRepository
    {
        void AddBudget(Budget budget);
        Task ChangePrice(decimal price, int eventId, BudgetPrices budgetPrice);

        // Budget GetBudget(int id);
        bool CheckStatus(int eventId);

        Task<Budget> GetBudget(int eventId);

    }
}
