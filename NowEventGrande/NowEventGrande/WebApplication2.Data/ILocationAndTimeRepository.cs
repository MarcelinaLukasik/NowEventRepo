using WebApplication2.Models;

namespace WebApplication2.Data
{
    public interface ILocationAndTimeRepository
    {
        void SaveLocation(EventAddress eventAddress);
        EventAddress GetLocation(int eventId);
        void SaveDateAndTime(Event eventById, DateTime date, DateTime start, DateTime end);
    }
}
