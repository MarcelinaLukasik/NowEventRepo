using WebApplication2.Models;

namespace WebApplication2.Data
{
    public class LocationRepository : ILocationRepository
    {
        private readonly AppDbContext _appDbContext;

        public LocationRepository(AppDbContext appDbContext)
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

    }
}
