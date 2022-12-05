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
        EventAddress GetLocation(int id);
        void SaveLocation(EventAddress eventAddress);
        bool VerifyLocation(EventAddress eventAddress);
        EventAddress GetLocationAddress(int eventId);
        // string? GetLocationHours(int eventId);
    }
}
