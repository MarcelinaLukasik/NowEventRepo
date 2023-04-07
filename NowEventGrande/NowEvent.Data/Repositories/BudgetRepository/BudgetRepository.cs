using NowEvent.Models;

namespace NowEvent.Data
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
            Budget budget = new Budget();
            budget.Total = 0;
            budget.RentPrice = 0;
            budget.DecorationPrice = 0;
            budget.FoodPrice = 0;
            budget.EventId = eventId;
            return budget;
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
                    default:
                        throw new ArgumentOutOfRangeException(nameof(budgetPrice), budgetPrice, null);
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

        public decimal GetBudgetPrice(int eventId, BudgetPrices priceType)
        {
            switch (priceType)
            {
                case BudgetPrices.Rent:
                    var rentPrice = _appDbContext.Budget.Where(x => x.EventId == eventId)
                        .Select(x => x.RentPrice).FirstOrDefault();
                    return rentPrice;
                case BudgetPrices.Decoration:
                    var decorPrice = _appDbContext.Budget.Where(x => x.EventId == eventId)
                        .Select(x => x.DecorationPrice).FirstOrDefault();
                    return decorPrice;
                case BudgetPrices.Food:
                    var foodPrice = _appDbContext.Budget.Where(x => x.EventId == eventId)
                        .Select(x => x.FoodPrice).FirstOrDefault();
                    return foodPrice;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public Dictionary<BudgetPrices, decimal> GetAllPrices(int eventId)
        {
            decimal rentPrice = GetBudgetPrice(eventId, BudgetPrices.Rent);
            decimal decorPrice = GetBudgetPrice(eventId, BudgetPrices.Decoration);
            decimal foodPrice = GetBudgetPrice(eventId, BudgetPrices.Food);
            Dictionary<BudgetPrices, decimal> pricesDict =
                new Dictionary<BudgetPrices, decimal> { { BudgetPrices.Rent, rentPrice },
                    { BudgetPrices.Decoration, decorPrice },
                    { BudgetPrices.Food, foodPrice }
                };
            return pricesDict;
        }
    }
}
