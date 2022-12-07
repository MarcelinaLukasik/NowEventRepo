using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.Data
{
    public class BudgetRepository : IBudgetRepository
    {
        private readonly AppDbContext _appDbContext;

        public BudgetRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void AddBudget(Budget budget)
        {
            var result = _appDbContext.Budget.Count(x => x.EventId == budget.EventId);
            if (result == 0)
            {
                _appDbContext.Budget.Add(budget);
                _appDbContext.SaveChanges();
            }
        }
        public async Task ChangePrice(decimal price, int eventId, BudgetPrices budgetPrice)
        {
            var budgedToChange = _appDbContext.Budget.FirstOrDefault(x => x.EventId == eventId);
            if (budgedToChange != null)
            {
                switch (budgetPrice)
                {
                    case BudgetPrices.Food:
                        budgedToChange.FoodPrice = price;
                        break;
                    case BudgetPrices.Decoration:
                        budgedToChange.DecorationPrice = price;
                        break;
                    case BudgetPrices.Rent:
                        budgedToChange.RentPrice = price;
                        break;
                }
                budgedToChange.Total = budgedToChange.RentPrice + budgedToChange.DecorationPrice + budgedToChange.FoodPrice;
                await _appDbContext.SaveChangesAsync();
            }
        }

        public async Task<Budget> GetBudget(int eventId)
        {
            return _appDbContext.Budget.FirstOrDefault(x => x.EventId == eventId);
    
        }

        public bool CheckStatus(int eventId)
        {
            var rentPrice = _appDbContext.Budget.Where(x => x.EventId == eventId).Select(x => x.RentPrice).FirstOrDefault();
            var decorPrice = _appDbContext.Budget.Where(x => x.EventId == eventId).Select(x => x.DecorationPrice).FirstOrDefault();
            var foodPrice = _appDbContext.Budget.Where(x => x.EventId == eventId).Select(x => x.FoodPrice).FirstOrDefault();

            return rentPrice != 0 && decorPrice != 0 && foodPrice != 0;
        }
    }
}
