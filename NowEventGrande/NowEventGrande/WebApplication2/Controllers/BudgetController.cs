using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Data;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BudgetController : ControllerBase
    {
        private readonly IBudgetRepository _budgetRepository;

        public BudgetController(IBudgetRepository budgetRepository)
        {
            _budgetRepository = budgetRepository;
        }

        [HttpPatch("{eventId:int}/Update/RentPrice")]
        public async Task<IActionResult> UpdateRentPrice(int eventId, [FromBody] string rentPrice)
        {
            var isDecimal = decimal.TryParse(rentPrice, out decimal price);
            if (isDecimal)
            {
                await _budgetRepository.ChangePrice(price, eventId, BudgetPrices.Rent);
                return Ok(rentPrice);
            }
            else return BadRequest(rentPrice);

        }

        [HttpPatch("{eventId:int}/Update/DecorationPrice")]
        public async Task<IActionResult> UpdateDecorPrice(int eventId, [FromBody] decimal decorationPrice)
        {
            await _budgetRepository.ChangePrice(decorationPrice, eventId, BudgetPrices.Decoration);
            return Ok(decorationPrice);
        }

        [HttpPatch("{eventId:int}/Update/FoodPrice")]
        public async Task<IActionResult> UpdateFoodPrice(int eventId, [FromBody] decimal foodPrice)
        {
            await _budgetRepository.ChangePrice(foodPrice, eventId, BudgetPrices.Food);
            return Ok(foodPrice);
        }

        [HttpGet("{id:int}/GetBudget")]
        public async Task<Budget> GetBudget(int id)
        {
            Budget budget = await _budgetRepository.GetBudget(id);
            return budget;
        }
    }
}
