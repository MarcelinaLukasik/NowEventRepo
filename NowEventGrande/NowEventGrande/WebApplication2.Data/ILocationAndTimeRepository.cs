using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.Data
{
    public interface ILocationAndTimeRepository
    {
        void SaveLocation(EventAddress eventAddress);
        EventAddress GetLocation(int eventId);
        // string? GetLocationHours(int eventId);
        void SaveDateAndTime(Event eventById, DateTime date, DateTime start, DateTime end);
    }
}
