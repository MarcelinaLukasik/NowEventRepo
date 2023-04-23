using NowEvent.Models;
using NowEvent.Models.Constants;

namespace NowEvent.Services.EventService
{
    public interface IEventService
    {
        int AddEvent(Event newEvent);
        Task<bool> SetEventDateAndTime(int id, Dictionary<string, string> formattedDateInfo);
        Task<string> GetStatus(int id);
        Task<Dictionary<string, string>> GetInfo(int id);
        bool ManageEventData(int id, string dataToChange, EventData eventDataCol);
        IQueryable GetEventsByUserId(string id);
        bool CheckIfLargeSize(int id);
        DateTime GetEventStartDate(int id);
    }
}
