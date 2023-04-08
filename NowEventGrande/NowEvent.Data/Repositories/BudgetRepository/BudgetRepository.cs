using NowEvent.Models;
using NowEvent.Models.Constants;

namespace NowEvent.Data.Repositories.BudgetRepository
{
    public class BudgetRepository : IBudgetRepository
    {
        private readonly AppDbContext _appDbContext;

        public BudgetRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public Budget CreateBudget(int eventId)
        {
            Budget budget = new Budget
            {
                Total = 0,
                RentPrice = 0,
                DecorationPrice = 0,
                FoodPrice = 0,
                EventId = eventId
            };
            return budget;
        }

        public void AddBudget(Budget budget)
        {
            var result = _appDbContext.Budget.Count(x => x.EventId == budget.EventId);
            if (result != 0) return;
            _appDbContext.Budget.Add(budget);
            _appDbContext.SaveChanges();
        }
        public async Task ChangePrice(decimal price, int eventId, BudgetOptions budgetOption)
        {
            var budgedToChange = _appDbContext.Budget.FirstOrDefault(x => x.EventId == eventId);
            if (budgedToChange != null)
            {
                switch (budgetOption)
                {
                    case BudgetOptions.Food:
                        budgedToChange.FoodPrice = price;
                        break;
                    case BudgetOptions.Decoration:
                        budgedToChange.DecorationPrice = price;
                        break;
                    case BudgetOptions.Rent:
                        budgedToChange.RentPrice = price;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(budgetOption), budgetOption, null);
                }
                budgedToChange.Total = budgedToChange.RentPrice 
                                       + budgedToChange.DecorationPrice 
                                       + budgedToChange.FoodPrice;
                await _appDbContext.SaveChangesAsync();
            }
        }

        public async Task<Budget> GetBudget(int eventId)
        {
            return _appDbContext.Budget.FirstOrDefault(x => x.EventId == eventId);
        }

        public decimal GetBudgetPrice(int eventId, BudgetOptions optionType)
        {
            switch (optionType)
            {
                case BudgetOptions.Rent:
                    var rentPrice = _appDbContext.Budget.Where(x => x.EventId == eventId)
                        .Select(x => x.RentPrice).FirstOrDefault();
                    return rentPrice;
                case BudgetOptions.Decoration:
                    var decorPrice = _appDbContext.Budget.Where(x => x.EventId == eventId)
                        .Select(x => x.DecorationPrice).FirstOrDefault();
                    return decorPrice;
                case BudgetOptions.Food:
                    var foodPrice = _appDbContext.Budget.Where(x => x.EventId == eventId)
                        .Select(x => x.FoodPrice).FirstOrDefault();
                    return foodPrice;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public Dictionary<BudgetOptions, decimal> GetAllPrices(int eventId)
        {
            decimal rentPrice = GetBudgetPrice(eventId, BudgetOptions.Rent);
            decimal decorPrice = GetBudgetPrice(eventId, BudgetOptions.Decoration);
            decimal foodPrice = GetBudgetPrice(eventId, BudgetOptions.Food);
            Dictionary<BudgetOptions, decimal> pricesDict =
                new Dictionary<BudgetOptions, decimal> { { BudgetOptions.Rent, rentPrice },
                    { BudgetOptions.Decoration, decorPrice },
                    { BudgetOptions.Food, foodPrice }
                };
            return pricesDict;
        }
    }
}
