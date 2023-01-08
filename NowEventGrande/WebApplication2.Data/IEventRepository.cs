using NowEvent.Models;

namespace NowEvent.Data
{
    public interface IEventRepository
    {
        int AddEvent(Event newEvent);
        Event GetEventById(int id);
        IQueryable GetEventsByUserId(string id);
        bool SetEventDateAndTime(int id, Dictionary<string, string> formattedDateInfo);
        bool CheckDateAndTimeByEventId(int id);
        DateTime GetEventStartDate(int id);
        void SetStatus(int id, string status);
        string GetStatus(int id);
        Dictionary<string, string> GetInfo(int id);
        bool ManageEventData(int id, string dataToChange, EventData eventDataCol);
        void SetEventData(Event eventById, string dataToChange, EventData eventDataCol);
        public PagedResult<Event> GetAll(OfferQuery query);
        bool CheckIfLargeSize(int id);
        DateTime GetEventTimeStage(int id, EventTimeStages eventTimeStage);
    }

}
