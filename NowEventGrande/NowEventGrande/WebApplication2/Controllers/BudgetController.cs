using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Data;
using WebApplication2.Models;
using WebApplication2.Services.VerificationService;

namespace WebApplication2.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BudgetController : ControllerBase
    {
        private readonly IBudgetRepository _budgetRepository;
        private readonly IVerificationService _verificationService;

        public BudgetController(IBudgetRepository budgetRepository, IVerificationService verificationService)
        {
            _budgetRepository = budgetRepository;
            _verificationService = verificationService;
        }

        [HttpPatch("{eventId:int}/Update/RentPrice")]
        public async Task<IActionResult> UpdateRentPrice(int eventId, [FromBody] string rentPrice)
        {
            bool validPrice = _verificationService.VerifyBudgetPrice(rentPrice);
            // var isDecimal = decimal.TryParse(rentPrice, out decimal price);
            if (validPrice)
            {
                await _budgetRepository.ChangePrice(decimal.Parse(rentPrice), eventId, BudgetPrices.Rent);
                return Ok(rentPrice);
            }
            else return BadRequest(rentPrice);

        }

        [HttpPatch("{eventId:int}/Update/DecorationPrice")]
        public async Task<IActionResult> UpdateDecorPrice(int eventId, [FromBody] string decorationPrice)
        {
            bool validPrice = _verificationService.VerifyBudgetPrice(decorationPrice);
            if (validPrice)
            {
                await _budgetRepository.ChangePrice(decimal.Parse(decorationPrice), eventId, BudgetPrices.Decoration);
                return Ok(decorationPrice);
            }
            else return BadRequest(decorationPrice);
        }

        [HttpPatch("{eventId:int}/Update/FoodPrice")]
        public async Task<IActionResult> UpdateFoodPrice(int eventId, [FromBody] string foodPrice)
        {
            bool validPrice = _verificationService.VerifyBudgetPrice(foodPrice);
            if (validPrice)
            {
                await _budgetRepository.ChangePrice(decimal.Parse(foodPrice), eventId, BudgetPrices.Food);
                return Ok(foodPrice);
            }
            else return BadRequest(foodPrice);

        }

        [HttpGet("{id:int}/GetBudget")]
        public async Task<Budget> GetBudget(int id)
        {
            Budget budget = await _budgetRepository.GetBudget(id);
            return budget;
        }
    }
}
