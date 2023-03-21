using Microsoft.AspNetCore.Mvc;
using NowEvent.Data;
using NowEvent.Models;
using NowEvent.Services.VerificationService;

namespace NowEvent.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProgressController : ControllerBase
    {
        private readonly ILogger<EventsController> _logger;
        private readonly IGuestRepository _guestRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IVerificationService _verificationService;
        public ProgressController(ILogger<EventsController> logger, IGuestRepository guestRepository,
            IEventRepository eventRepository, IVerificationService verificationService)
        {
            _logger = logger;
            _guestRepository = guestRepository;
            _eventRepository = eventRepository;
            _verificationService = verificationService;
        }

        
        [HttpGet("{id:int}/CheckStatus")]
        public async Task<bool> CheckIfComplete(int id)
        {
            var guests = _guestRepository.AllGuestsByEventId(id);
            if (!guests.Any())
            {
                _eventRepository.SetStatus(id, EventStatuses.Incomplete);
                return false;
            }

            var budgetStatus = _verificationService.CheckBudgetFullStatus(id);
            if (!budgetStatus)
            {
                _eventRepository.SetStatus(id, EventStatuses.Incomplete);
                return false;
            }

            var checkDate = await _eventRepository.CheckDateAndTimeByEventId(id);
            if (!checkDate)
            {
                _eventRepository.SetStatus(id, EventStatuses.Incomplete);
                return false;
            }
            else
            {
                _eventRepository.SetStatus(id, EventStatuses.Completed);
                return true;
            }
        }

        [HttpGet("{id:int}/GetChecklistProgress")]
        public async Task<int> GetChecklistProgress(int id)
        {
            var checklistCount = 0;
            var guests = _guestRepository.AllGuestsByEventId(id);
            if (guests.Any())
            {
                checklistCount++;
            }

            var budgetStatus = _verificationService.CheckBudgetFullStatus(id);
            if (budgetStatus)
            {
                checklistCount++;
            }
            var checkDate = await _eventRepository.CheckDateAndTimeByEventId(id);
            if (checkDate)
            {
                checklistCount++;
            }

            return checklistCount;
        }
    }
}
