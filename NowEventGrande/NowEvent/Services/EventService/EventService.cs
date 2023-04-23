using NowEvent.Data.Repositories.EventRepository;
using NowEvent.Data.Repositories.LocationAndTimeRepository;
using NowEvent.Models;
using NowEvent.Models.Constants;
using NowEvent.Services.VerificationService;

namespace NowEvent.Services.EventService
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly ILocationAndTimeRepository _locationAndTimeRepository;
        private readonly IVerificationService _verificationService;
        public EventService(IEventRepository eventRepository,
            ILocationAndTimeRepository locationAndTimeRepository,
            IVerificationService verificationService)
        {
            _eventRepository = eventRepository;
            _locationAndTimeRepository = locationAndTimeRepository;
            _verificationService = verificationService;
        }

        public int AddEvent(Event newEvent)
        {
            int newEventId = _eventRepository.AddEvent(newEvent);
            return newEventId;
        }

        public async Task<bool> SetEventDateAndTime(int id, Dictionary<string, string> formattedDateInfo)
        {
            var date = DateTime.Parse(formattedDateInfo[EventInfoFields.Date]);
            var start = DateTime.Parse(formattedDateInfo[Date.StartTime]);
            var end = DateTime.Parse(formattedDateInfo[Date.EndTime]);

            if (_verificationService.VerifyEventDateAndTime(formattedDateInfo))
            {
                var eventById = await _eventRepository.GetEventByIdAsync(id);
                int result = DateTime.Compare(date, DateTime.Now);
                if (result < 0)
                {
                    return false;
                }
                else
                {
                    _locationAndTimeRepository.SaveDateAndTime(eventById, date, start, end);
                    return true;
                }

            }
            else return false;
        }

        public async Task<string> GetStatus(int id)
        {
            return await _eventRepository.GetStatus(id);
        }
        public async Task<Dictionary<string, string>> GetInfo(int id)
        {
            Dictionary<string, string> info = new Dictionary<string, string>();
            var eventById = await _eventRepository.GetEventByIdAsync(id);
            var eventAddress = await _locationAndTimeRepository.GetEventAddress(id);
            info[EventInfoFields.Type] = eventById.Type;
            info[EventInfoFields.Name] = eventById.Name;
            info[EventInfoFields.Status] = eventById.Status;
            info[EventInfoFields.Address] = eventAddress;
            return info;
        }

        public bool ManageEventData(int id, string dataToChange, EventData eventDataCol)
        {
            return _eventRepository.ManageEventData(id, dataToChange, eventDataCol);
        }

        public IQueryable GetEventsByUserId(string id)
        {
            return _eventRepository.GetEventsByUserId(id);
        }

        public bool CheckIfLargeSize(int id)
        {
            return _eventRepository.CheckIfLargeSize(id);
        }

        public DateTime GetEventStartDate(int id)
        {
            return _eventRepository.GetEventStartDate(id);
        }
    }
}
