using NowEvent.Models;
using NowEvent.Models.Constants;

namespace NowEvent.Data.Repositories.EventRepository
{
    public interface IEventRepository
    {
        int AddEvent(Event newEvent);
        Task<Event> GetEventByIdAsync(int id);
        Task<bool> CheckDateAndTimeByEventId(int id);
        DateTime GetEventStartDate(int id);
        Task SetStatus(int id, string status);
        Task<string> GetStatus(int id);
   
        bool ManageEventData(int id, string dataToChange, EventData eventDataCol);
        public Task<PagedResult<Event>> GetAll(OfferQuery query);
        public Task<List<Event>> GetAllOffers();
        bool CheckIfLargeSize(int id);
        DateTime GetEventTimeStage(int id, EventTimeStages eventTimeStage);
        IQueryable GetEventsByUserId(string id);
    }

}
