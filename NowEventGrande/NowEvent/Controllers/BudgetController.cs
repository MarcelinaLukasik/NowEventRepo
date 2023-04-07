using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NowEvent.Data;
using NowEvent.Models;
using NowEvent.Services.VerificationService;

namespace NowEvent.Controllers
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

        [HttpPatch("{eventId:int}/Update/{typeToChange}")]
        public async Task<IActionResult> UpdateRentPrice(int eventId, string typeToChange, [FromBody] string rentPrice)
        {
            bool validPrice = _verificationService.VerifyBudgetPrice(rentPrice);
            if (validPrice)
            {
                switch (typeToChange)
                {
                    case "RentPrice":
                        await _budgetRepository.ChangePrice(decimal.Parse(rentPrice), 
                            eventId, BudgetPrices.Rent);
                        break;
                    case "DecorationPrice":
                        await _budgetRepository.ChangePrice(decimal.Parse(rentPrice), 
                            eventId, BudgetPrices.Decoration);
                        break;
                    case "FoodPrice":
                        await _budgetRepository.ChangePrice(decimal.Parse(rentPrice), 
                            eventId, BudgetPrices.Food);
                        break;
                }
                return Ok(rentPrice);
            }
            else return BadRequest(rentPrice);

        }

        [HttpGet("{id:int}/GetBudget")]
        public async Task<Budget> GetBudget(int id)
        {
            Budget budget = await _budgetRepository.GetBudget(id);
            return budget;
        }
    }
}
