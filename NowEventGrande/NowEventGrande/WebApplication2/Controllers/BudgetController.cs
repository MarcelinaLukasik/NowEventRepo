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

        [HttpPost("GetBudget")]
        public IActionResult GetBudget([FromBody] Budget budget)
        {
            _budgetRepository.AddBudget(budget);
            return Ok(budget);
        }

        [HttpPatch("{eventId}/Update/RentPrice")]
        public async Task<IActionResult> UpdateRentPrice(int eventId, [FromBody] string rentPrice)
        {
            var isDecimal = decimal.TryParse(rentPrice, out decimal price);
            if (isDecimal)
            {
                await _budgetRepository.ChangeRentPrice(price, eventId);
                return Ok(rentPrice);
            }
            else
            {
                return BadRequest(rentPrice);
            }

        }

        [HttpPatch("{eventId}/Update/DecorationPrice")]
        public async Task<IActionResult> UpdateDecorPrice(int eventId, [FromBody] decimal decorationPrice)
        {
            await _budgetRepository.ChangeDecorPrice(decorationPrice, eventId);
            return Ok(decorationPrice);
        }

        [HttpPatch("{eventId}/Update/FoodPrice")]
        public async Task<IActionResult> UpdateFoodPrice(int eventId, [FromBody] decimal foodPrice)
        {
            await _budgetRepository.ChangeFoodPrice(foodPrice, eventId);
            return Ok(foodPrice);
        }

        [HttpGet("{id}/GetBudgetStats")]
        public async Task<Budget> GetBudgetStats(int id)
        {
            Budget budget = await _budgetRepository.GetStats(id);
            return budget;
        }
    }
}
