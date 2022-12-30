using Microsoft.AspNetCore.Mvc;
using WebApplication2.Data;
using WebApplication2.Models;
using WebApplication2.Services.VerificationService;

namespace WebApplication2.Controllers
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

        //TODO checkStatus, then setStatus based on checkStatus return value, ask where enum should be
        [HttpGet("{id:int}/CheckStatus")]
        public bool CheckIfComplete(int id)
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

            if (!_eventRepository.CheckDateAndTimeByEventId(id))
            {
                _eventRepository.SetStatus(id, EventStatuses.Incomplete);
                return false;
            }
            else
            {
                //TODO move event statuses to enum
                _eventRepository.SetStatus(id, EventStatuses.Completed);
                return true;
            }
        }

        [HttpGet("{id:int}/GetChecklistProgress")]
        public int GetChecklistProgress(int id)
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

            if (_eventRepository.CheckDateAndTimeByEventId(id))
            {
                checklistCount++;
            }

            return checklistCount;
        }
    }
}
