using NowEvent.Data.Repositories.BudgetRepository;
using NowEvent.Models;
using NowEvent.Models.Constants;

namespace NowEvent.Services.BudgetService
{
    public class BudgetService : IBudgetService
    {
        private readonly IBudgetRepository _budgetRepository;
        public BudgetService(IBudgetRepository budgetRepository)
        {
            _budgetRepository = budgetRepository;
        }

        public async Task ChangePrice(decimal price, int eventId, BudgetOptions budgetOption)
        {
            await _budgetRepository.ChangePrice(price, eventId, budgetOption);
        }

        public Task<Budget> GetBudget(int eventId)
        {
            return _budgetRepository.GetBudget(eventId);
        }
        public Dictionary<BudgetOptions, decimal> GetAllPrices(int eventId)
        {
            decimal rentPrice = _budgetRepository.GetBudgetPrice(eventId, BudgetOptions.Rent);
            decimal decorPrice = _budgetRepository.GetBudgetPrice(eventId, BudgetOptions.Decoration);
            decimal foodPrice = _budgetRepository.GetBudgetPrice(eventId, BudgetOptions.Food);
            Dictionary<BudgetOptions, decimal> pricesDict =
                new Dictionary<BudgetOptions, decimal> { { BudgetOptions.Rent, rentPrice },
                    { BudgetOptions.Decoration, decorPrice },
                    { BudgetOptions.Food, foodPrice }
                };
            return pricesDict;
        }
    }
}
