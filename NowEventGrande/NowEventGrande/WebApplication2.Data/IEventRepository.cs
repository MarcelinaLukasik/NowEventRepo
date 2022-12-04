using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.Data
{
    public interface IEventRepository
    {
        // IEnumerable<Event> AllEvents { get; }
        // void RemoveEvent(int id);
        int AddEvent(Event newEvent);
        Event GetEventById(int id);
        bool SaveEventDateAndTime(int id, Dictionary<string, string> dateInfo);
        bool CheckDateAndTime(int id);

        DateTime GetEventStartDate(int id);
        void SetStatus(int id, string status);
        string GetStatus(int id);

        Dictionary<string, string> GetInfo(int id);
        DateTime GetEventStartTime(int id);
        DateTime GetEventEndTime(int id);
    }

}
