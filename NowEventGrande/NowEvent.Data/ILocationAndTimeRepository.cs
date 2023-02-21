using NowEvent.Models;

namespace NowEvent.Data
{
    public interface ILocationAndTimeRepository
    {
        void SaveLocation(EventAddress eventAddress);
        EventAddress GetLocation(int eventId);
        void SaveDateAndTime(Event eventById, DateTime date, DateTime start, DateTime end);
        Task<string> GetEventAddress(int id);
    }
}
