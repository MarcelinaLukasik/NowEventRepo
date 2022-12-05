using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.Data
{
    public class EventRepository : IEventRepository
    {
        private readonly AppDbContext _appDbContext;

        public EventRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public int AddEvent(Event newEvent)
        {
            newEvent.Status = "Incomplete";
            _appDbContext.Events.Add(newEvent);
            _appDbContext.SaveChanges();

            Budget budget = CreateBudget(newEvent.Id);
            _appDbContext.Budget.Add(budget);
            _appDbContext.SaveChanges();
            return newEvent.Id;
        }

        public IEnumerable<Event> GetOffersWithInCompleteStatus()
        {
            return _appDbContext.Events.Where(x => x.Status == "Incomplete");
        }

        public Budget CreateBudget(int eventId)
        {
            Budget budget = new Budget();
            budget.Total = 0;
            budget.RentPrice = 0;
            budget.DecorationPrice = 0;
            budget.FoodPrice = 0;
            budget.EventId = eventId;
            return budget;
        }

        public Event GetEventById(int id)
        {
            return _appDbContext.Events.FirstOrDefault(x => x.Id == id);
        }

        public bool SaveEventDateAndTime(int id, Dictionary<string, string>  dateInfo)
        {
            var eventById = GetEventById(id);
            var date = DateTime.Now;
            var start = DateTime.Now;
            var end = DateTime.Now;
            bool correct = DateTime.TryParse(dateInfo["Date"], out date);
            var startTime = dateInfo["StartHour"] + ":" + dateInfo["StartMinutes"] + " " + dateInfo["TimeOfDayStart"];
            var endTime = dateInfo["EndHour"] + ":" + dateInfo["EndMinutes"] + " " + dateInfo["TimeOfDayEnd"];

            //TODO check if date is not older than current day
            if (correct)
            {
                int result = DateTime.Compare(date, DateTime.Now);
                if (result < 0)
                {
                    return false;
                }
                else
                    eventById.Date = date;
            }
            else return false;

            bool correctStartTime = DateTime.TryParse(startTime, out start);
            if (correctStartTime)
            {
                DateTime newDateTime = date.Date.Add(start.TimeOfDay);
                eventById.EventStart = newDateTime;
            }
            else return false;

            bool correctEndTime = DateTime.TryParse(endTime, out end);
            if (correctEndTime)
            {
                eventById.EventEnd = end;
            }
            else return false;

            _appDbContext.SaveChanges();
            return true;
        }

        public bool CheckDateAndTime(int id)
        {
            var eventById = GetEventById(id);
            var eventDate = eventById.Date;
            if (eventDate > DateTime.Now)
            {
                return true;
            }
            return false;
        }

        public DateTime GetEventStartDate(int id)
        {
            var eventById = GetEventById(id);
            return eventById.EventStart;
        }

        public void SetStatus(int id, string status)
        {
            var eventById = GetEventById(id);
            eventById.Status = status;
            _appDbContext.SaveChanges();
        }

        public string GetStatus(int id)
        {
            var eventById = GetEventById(id);
            return eventById.Status;
        }

        public Dictionary<string, string> GetInfo(int id)
        {
            Dictionary<string, string> info = new Dictionary<string, string>();
            var eventById = GetEventById(id);
            info["Type"] = eventById.Type;
            info["Name"] = eventById.Name;
            info["Status"] = eventById.Status;
            return info;
        }

        public DateTime GetEventStartTime(int id)
        {
            return _appDbContext.Events.Where(x => x.Id == id).Select(y => y.EventStart).FirstOrDefault();
        }

        public DateTime GetEventEndTime(int id)
        {
            return _appDbContext.Events.Where(x => x.Id == id).Select(y => y.EventEnd).FirstOrDefault();
        }

        public bool SetSize(int id, string size)
        {
            var eventById = _appDbContext.Events.FirstOrDefault(x => x.Id == id);
            if (eventById != null)
            {
                eventById.Size = size;
                _appDbContext.SaveChanges();
                return true;
            }
            else return false;
        }

        public bool SetSizeRange(int id, string sizeRange)
        {
            var eventById = _appDbContext.Events.FirstOrDefault(x => x.Id == id);
            if (eventById != null)
            {
                eventById.SizeRange = sizeRange;
                _appDbContext.SaveChanges();
                return true;
            }
            else return false;
        }
    }
}
