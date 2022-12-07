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
        bool SetEventDateAndTime(int id, Dictionary<string, string> dateInfo);
        bool CheckDateAndTimeByEventId(int id);

        DateTime GetEventStartDate(int id);
        void SetStatus(int id, string status);
        string GetStatus(int id);

        Dictionary<string, string> GetInfo(int id);
        public IEnumerable<Event> GetOffersWithInCompleteStatus();
        DateTime GetEventStartTime(int id);
        DateTime GetEventEndTime(int id);
        bool ManageEventData(int id, string dataToChange, EventData eventDataCol);
        void SetEventData(Event eventById, string dataToChange, EventData eventDataCol);
    }

}
