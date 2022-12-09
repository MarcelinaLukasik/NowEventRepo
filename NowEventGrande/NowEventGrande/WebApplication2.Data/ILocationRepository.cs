using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.Data
{
    public interface ILocationRepository
    {
        void SaveLocation(EventAddress eventAddress);
        EventAddress GetLocation(int eventId);
        // string? GetLocationHours(int eventId);
    }
}
