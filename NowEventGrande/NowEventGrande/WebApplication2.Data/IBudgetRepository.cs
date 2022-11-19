using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication2.Data
{
    public interface IBudgetRepository
    {
        void AddBudget(Budget budget);
        Task ChangeRentPrice(decimal rentPrice, int Id);
        Task ChangeDecorPrice(decimal decorationPrice, int Id);
        Task ChangeFoodPrice(decimal foodPrice, int Id);

        // Budget GetBudget(int id);
        bool CheckStatus(int eventId);

        Task<Budget> GetStats(int eventId);
    }
}
