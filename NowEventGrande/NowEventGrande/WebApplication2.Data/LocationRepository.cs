using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
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
                _appDbContext.EventAddress.Add(eventAddress);
            else
                address.FullAddress = eventAddress.FullAddress;
            
            _appDbContext.SaveChanges();
        }

    }
}
