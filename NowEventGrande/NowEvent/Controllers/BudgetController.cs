using Microsoft.AspNetCore.Mvc;
using NowEvent.Data;
using NowEvent.Data.Repositories.BudgetRepository;
using NowEvent.Models;
using NowEvent.Models.Constants;
using NowEvent.Services.BudgetService;
using NowEvent.Services.VerificationService;

namespace NowEvent.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BudgetController : ControllerBase
    {
        private readonly IVerificationService _verificationService;
        private readonly IBudgetService _budgetService;
        public BudgetController(IVerificationService verificationService, IBudgetService budgetService)
        {
            _verificationService = verificationService;
            _budgetService = budgetService;
        }

        [HttpPatch("{eventId:int}/Update/{typeToChange}")]
        public async Task<IActionResult> UpdatePrices(int eventId, string typeToChange, [FromBody] string price)
        {
            bool validPrice = _verificationService.VerifyBudgetPrice(price);
            if (validPrice)
            {
                switch (typeToChange)
                {
                    case BudgetPrices.Rent:
                        await _budgetService.ChangePrice(decimal.Parse(price), 
                            eventId, BudgetOptions.Rent);
                        break;
                    case BudgetPrices.Decoration:
                        await _budgetService.ChangePrice(decimal.Parse(price), 
                            eventId, BudgetOptions.Decoration);
                        break;
                    case BudgetPrices.Food:
                        await _budgetService.ChangePrice(decimal.Parse(price), 
                            eventId, BudgetOptions.Food);
                        break;
                }
                return Ok(price);
            }
            return BadRequest(price);

        }

        [HttpGet("{id:int}/GetBudget")]
        public async Task<Budget> GetBudget(int id)
        {
            Budget budget = await _budgetService.GetBudget(id);
            return budget;
        }
    }
}
