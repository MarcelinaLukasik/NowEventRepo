using System;
using System.Collections.Generic;
using System.Linq;
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
        public EventAddress GetLocation(int id)
        {
            return _appDbContext.EventAddress.FirstOrDefault(x => x.Id == id);
        }
    }
}
