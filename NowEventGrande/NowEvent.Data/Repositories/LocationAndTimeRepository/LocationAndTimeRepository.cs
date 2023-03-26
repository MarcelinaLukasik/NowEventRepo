using Microsoft.EntityFrameworkCore;
using NowEvent.Models;

namespace NowEvent.Data
{
    public class LocationAndTimeRepository : ILocationAndTimeRepository
    {
        private readonly AppDbContext _appDbContext;

        public LocationAndTimeRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public EventAddress GetLocation(int eventId)
        {
            return _appDbContext.EventAddress.FirstOrDefault(x => x.EventId == eventId);
        }


        public void SaveLocation(EventAddress eventAddress)
        {
            var address = _appDbContext.EventAddress.FirstOrDefault(x => x.EventId == eventAddress.EventId);
            if (address == null)
            {
                _appDbContext.EventAddress.Add(eventAddress);
                _appDbContext.SaveChanges();
            }
                
            else if (address != eventAddress)
            {
                address.FullAddress = eventAddress.FullAddress;
                address.Latitude = eventAddress.Latitude;
                address.Longitude = eventAddress.Longitude;
                address.PlaceId = eventAddress.PlaceId;
                address.PlaceOpeningHours = eventAddress.PlaceOpeningHours;
                address.PlaceStatus = eventAddress.PlaceStatus;
                _appDbContext.SaveChanges();
            }
            
        }

        public void SaveDateAndTime(Event eventById, DateTime date, DateTime start, DateTime end)
        {
            eventById.Date = date;
            eventById.EventStart = date.Date.Add(start.TimeOfDay);
            eventById.EventEnd = date.Date.Add(end.TimeOfDay);
            _appDbContext.SaveChanges();
        }

        public async Task<string> GetEventAddress(int id)
        {
            string address = await _appDbContext.EventAddress.Where(x => x.EventId == id)
                .Select(x => x.FullAddress).FirstOrDefaultAsync() ?? "No address set";
            return address;
        }

    }
}
