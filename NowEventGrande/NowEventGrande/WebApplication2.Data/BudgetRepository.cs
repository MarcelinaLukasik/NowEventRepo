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

        public async Task ChangeRentPrice(decimal rentPrice, int eventId)
        {
            var budgedToChange = _appDbContext.Budget.FirstOrDefault(x => x.EventId == eventId);
            budgedToChange.RentPrice = rentPrice;
            budgedToChange.Total = budgedToChange.RentPrice + budgedToChange.DecorationPrice + budgedToChange.FoodPrice;
            await _appDbContext.SaveChangesAsync();
        }

        public async Task ChangeDecorPrice(decimal decorationPrice, int eventId)
        {
            var budgedToChange = _appDbContext.Budget.FirstOrDefault(x => x.EventId == eventId);
            budgedToChange.DecorationPrice = decorationPrice;
            budgedToChange.Total = budgedToChange.RentPrice + budgedToChange.DecorationPrice + budgedToChange.FoodPrice;
            await _appDbContext.SaveChangesAsync();
        }

        public async Task ChangeFoodPrice(decimal foodPrice, int eventId)
        {
            var budgedToChange = _appDbContext.Budget.FirstOrDefault(x => x.EventId == eventId);
            budgedToChange.FoodPrice = foodPrice;
            budgedToChange.Total = budgedToChange.RentPrice + budgedToChange.DecorationPrice + budgedToChange.FoodPrice;
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<Budget> GetStats(int eventId)
        {
            return _appDbContext.Budget.FirstOrDefault(x => x.EventId == eventId);
    
        }

        public bool CheckStatus(int eventId)
        {
            var rentPrice = _appDbContext.Budget.Where(x => x.EventId == eventId).Select(x => x.RentPrice).FirstOrDefault();
            if (rentPrice == 0)
            {
                return false;
            }
            var decorPrice = _appDbContext.Budget.Where(x => x.EventId == eventId).Select(x => x.DecorationPrice).FirstOrDefault();
            if (decorPrice == 0)
            {
                return false;
            }
            var foodPrice = _appDbContext.Budget.Where(x => x.EventId == eventId).Select(x => x.FoodPrice).FirstOrDefault();
            if (foodPrice == 0)
            {
                return false;
            }
            else return true;
        }
    }
}
