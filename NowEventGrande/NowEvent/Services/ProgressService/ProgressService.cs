using NowEvent.Data;
using NowEvent.Models;
using NowEvent.Services.VerificationService;

namespace NowEvent.Services.ProgressService
{
    public class ProgressService : IProgressService
    {
        
        private readonly IEventRepository _eventRepository;
        private readonly IGuestRepository _guestRepository;
        private readonly IVerificationService _verificationService;
        private int _progressLevel;
        private int _fullProgress = 3;
        public ProgressService(IEventRepository eventRepository, IGuestRepository guestRepository, 
            IVerificationService verificationService)
        {
            _eventRepository = eventRepository;
            _guestRepository = guestRepository;
            _verificationService = verificationService;
            
        }
        public async Task<bool> CheckEventStatus(int id)
        {
            _progressLevel = await GetChecklistCount(id);
            if (_progressLevel != _fullProgress)
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

        public async Task<int> GetChecklistCount(int id)
        {
            var checklistCount = 0;
            bool isLargeScale = _eventRepository.CheckIfLargeSize(id);

            var guests = _guestRepository.AllGuestsByEventId(id);
            if (guests.Any() || isLargeScale)
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
