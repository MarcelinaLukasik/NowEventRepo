using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using NowEvent.Data.Repositories.BudgetRepository;
using NowEvent.Data.Repositories.LocationAndTimeRepository;
using NowEvent.Models;
using NowEvent.Models.Constants;

namespace NowEvent.Data.Repositories.EventRepository
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
        public async Task<List<Event>> GetAllOffers()
        {
            return await _appDbContext.Events
                .Where(x => x.Status == EventStatuses.Posted).ToListAsync();
        }

        public async Task<PagedResult<Event>> GetAll(OfferQuery query)
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

            var offers = await baseQuery
                .Skip(query.PageSize * (query.PageNumber - 1))
            .Take(query.PageSize)
            .ToListAsync();

            var totalItemsCount = baseQuery.Count();
            var result = new PagedResult<Event>(offers, totalItemsCount, query.PageSize, query.PageNumber);
            return result;
        }

        public async Task<Event> GetEventByIdAsync(int id)
        {
            return await _appDbContext.Events.FindAsync(id);
        }

        private Event GetEventById(int id)
        {
            return _appDbContext.Events.Find(id);
        }

        public IQueryable GetEventsByUserId(string id)
        {
            var events = _appDbContext.Events.Where(x => x.ClientId == id);
            return events;
        }

        public async Task<bool> CheckDateAndTimeByEventId(int id)
        {
            var eventId = await GetEventByIdAsync(id);
            return eventId.Date > DateTime.Now;
        }

        public DateTime GetEventStartDate(int id)
        {
            var eventById = GetEventById(id);
            return eventById.EventStart;
        }

        public async Task SetStatus(int id, string status)
        {
            var eventById = await GetEventByIdAsync(id);
            eventById.Status = status;
            await _appDbContext.SaveChangesAsync();
        }
        public async Task<string> GetStatus(int id)
        {
            var eventById = await GetEventByIdAsync(id);
            return eventById.Status;
        }
   
        public DateTime GetEventTimeStage(int id, EventTimeStages eventTimeStage)
        {
            switch (eventTimeStage)
            {
                case EventTimeStages.Start:
                    return _appDbContext.Events.Where(x => x.Id == id)
                        .Select(y => y.EventStart).FirstOrDefault();
                case EventTimeStages.End:
                    return _appDbContext.Events.Where(x => x.Id == id)
                        .Select(y => y.EventEnd).FirstOrDefault();
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

        private void SetEventData(Event eventById, string dataToChange, EventData eventDataCol)
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
            return eventById!.Size == EventSizes.Large.ToString() && eventById.SizeRange != null;
        }
    }
}
