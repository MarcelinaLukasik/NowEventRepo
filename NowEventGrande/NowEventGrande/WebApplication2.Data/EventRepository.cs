using System.Linq.Expressions;
using WebApplication2.Models;

namespace WebApplication2.Data
{
    public class EventRepository : IEventRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IBudgetRepository _budgetRepository;
        private readonly ILocationAndTimeRepository _locationAndTimeRepository;

        public EventRepository(AppDbContext appDbContext, IBudgetRepository budgetRepository,
            ILocationAndTimeRepository locationAndTimeRepository)
        {
            _appDbContext = appDbContext;
            _budgetRepository = budgetRepository;
            _locationAndTimeRepository = locationAndTimeRepository;
        }
        public int AddEvent(Event newEvent)
        {
            newEvent.Status = EventStatuses.Incomplete;
            _appDbContext.Events.Add(newEvent);
            _appDbContext.SaveChanges();

            Budget budget = _budgetRepository.CreateBudget(newEvent.Id);
            _budgetRepository.AddBudget(budget);
            return newEvent.Id;
        }

        public PagedResult<Event> GetAll(OfferQuery query)
        {
            var baseQuery = _appDbContext.Events
                .Where(r => query.SearchPhrase == null || (r.Name.ToLower().Contains(query.SearchPhrase.ToLower())
                                                           || r.Size.ToLower().Contains(query.SearchPhrase.ToLower())
                                                           || r.Type.ToLower().Contains(query.SearchPhrase.ToLower())));
            if (!string.IsNullOrEmpty(query.SortBy))
            {
                var columnSelectors = new Dictionary<string, Expression<Func<Event, object>>>
                {
                    { nameof(Event.Name), r=> r.Name },
                    { nameof(Event.Size), r => r.Size },
                    { nameof(Event.Type), r=> r.Type },
                };

                var selectedColumn = columnSelectors[query.SortBy];
               baseQuery = query.SortDirection == SortDirection.ASC ?
                   baseQuery.OrderBy(selectedColumn)
                   : baseQuery.OrderByDescending(selectedColumn);
            }

            var offers = baseQuery
            // pomijamy oferty o określoną ilość
            .Skip(query.PageSize * (query.PageNumber - 1))
            // bierzemy określolną ilość ofert
            .Take(query.PageSize)
            .ToList();

            var totalItemsCount = baseQuery.Count();
            var result = new PagedResult<Event>(offers, totalItemsCount, query.PageSize, query.PageNumber);
            return result;
        }

        

        public Event GetEventById(int id)
        {
            return _appDbContext.Events.FirstOrDefault(x => x.Id == id);
        }

        public IQueryable GetEventsByUserId(string id)
        {
            return _appDbContext.Events.Where(x => x.ClientId == id).Select(x => new { x.Name, x.Status, x.Id});
        }


        public bool SetEventDateAndTime(int id, Dictionary<string, string> formattedDateInfo)
        {
            bool isDateCorrect = DateTime.TryParse(formattedDateInfo["Date"], out var date);
            bool correctStartTime = DateTime.TryParse(formattedDateInfo["StartTime"], out var start);
            bool correctEndTime = DateTime.TryParse(formattedDateInfo["EndTime"], out var end);

            if (isDateCorrect && correctStartTime && correctEndTime)
            {
                var eventById = GetEventById(id);
                int result = DateTime.Compare(date, DateTime.Now);
                if (result < 0)
                {
                    return false;
                }
                else
                {
                    _locationAndTimeRepository.SaveDateAndTime(eventById, date, start, end);
                    return true;
                }
                
            }
            else return false;
        }

        public bool CheckDateAndTimeByEventId(int id)
        {
            return GetEventById(id).Date > DateTime.Now;
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
        public DateTime GetEventTimeStage(int id, EventTimeStages eventTimeStage)
        {
            switch (eventTimeStage)
            {
                case EventTimeStages.Start:
                    return _appDbContext.Events.Where(x => x.Id == id).Select(y => y.EventStart).FirstOrDefault();
                case EventTimeStages.End:
                    return _appDbContext.Events.Where(x => x.Id == id).Select(y => y.EventEnd).FirstOrDefault();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public bool ManageEventData(int id, string dataToChange, EventData eventDataCol)
        {
            var eventById = _appDbContext.Events.FirstOrDefault(x => x.Id == id);
            bool isCorrect = eventById != null;
            if (isCorrect)
            {
#pragma warning disable CS8604
                SetEventData(eventById, dataToChange, eventDataCol);
#pragma warning restore CS8604
                _appDbContext.SaveChanges();
            }
            return isCorrect;
        }

        public void SetEventData(Event eventById, string dataToChange, EventData eventDataCol)
        {
            switch (eventDataCol)
            {
                case EventData.Size:
                    eventById.Size = dataToChange;
                    break;

                case EventData.SizeRange:
                    eventById.SizeRange = dataToChange;
                    break;

                case EventData.Theme:
                    eventById.Theme = dataToChange;
                    break;
            }
        }

        public bool CheckIfLargeSize(int id)
        {
            var eventById = _appDbContext.Events.FirstOrDefault(x => x.Id == id);
            return eventById.Size == "Large";
        }
    }
}
