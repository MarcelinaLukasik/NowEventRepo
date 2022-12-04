using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Diagnostics;
using WebApplication2.Data;
using WebApplication2.Models;


namespace WebApplication2.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly ILogger<EventsController> _logger;
        private readonly IGuestRepository _guestRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IBudgetRepository _budgetRepository;
        private readonly IOfferRepository _offerRepository;

        public EventsController(ILogger<EventsController> logger, IGuestRepository guestRepository, IEventRepository eventRepository, IBudgetRepository budgetRepository, IOfferRepository offerRepository)
        {
            _logger = logger;
            _guestRepository = guestRepository;
            _eventRepository = eventRepository;
            _budgetRepository = budgetRepository;
            _offerRepository = offerRepository;
        }



        [HttpPost("PostOffer")]
        public IActionResult AddOffer([FromBody] Offer offer)
        {
            _offerRepository.AddOffer(offer);
            return Ok(offer);
        }

        [HttpPost("GetGuest")]
        public IActionResult GetGuest([FromBody] Guest guest)
        {
            _guestRepository.AddGuest(guest);
            return Ok(guest);
        }

        [HttpGet("{id}/all")]
        public IEnumerable<Guest> GetAllGuests(int id)
        {
            return _guestRepository.AllGuestsByEventId(id);
        }

        [HttpGet("{id}/all/descending")]
        public IEnumerable<Guest> GuestsSortedDescending(int id)
        {
            return  _guestRepository.SortDescending().Where(x => x.EventId == id).ToArray();
        }

        [HttpGet("{id}/all/ascending")]
        public IEnumerable<Guest> GuestsSortedAscending(int id)
        {
            return _guestRepository.SortAscending().Where(x => x.EventId == id).ToArray();
        }

        [HttpDelete("removeGuest/{id}")]
        public IActionResult RemoveGuest(int id)
        {
            _guestRepository.RemoveGuest(id);
            return Ok(id);
        }

        [HttpPost("CreateNewEvent")]
        public int CreateNewEvent([FromBody] Event newEvent)
        {
            int id = _eventRepository.AddEvent(newEvent);
            return id;
        }

        //TODO move to new controller, save to database
        [HttpPost("SaveRatings")]
        public IActionResult CreateNewEvent([FromBody] Rating rating)
        {
            var test = rating;
            return Ok(test);
        }


        [HttpGet("GetKey")]
        public string GetKey()
        {
            var key = System.IO.File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Key.txt"));
            var firstKey = key.Split(";")[0];
            return firstKey;

        }

        //TODO checkStatus, then setStatus based on checkStatus return value
        [HttpGet("{id}/CheckStatus")]
        public bool CheckIfComplete(int id)
        {
            var guests = _guestRepository.AllGuestsByEventId(id);
            if (!guests.Any())
            {
                _eventRepository.SetStatus(id, "Incomplete");
                return false;
            }
            var budgetStatus = _budgetRepository.CheckStatus(id);
            if (!budgetStatus)
            {
                _eventRepository.SetStatus(id, "Incomplete");
                return false;
            }
            if (!_eventRepository.CheckDateAndTime(id))
            {
                _eventRepository.SetStatus(id, "Incomplete");
                return false;
            }
            else
            {
                //TODO move event statuses to enum
                _eventRepository.SetStatus(id, "Completed");
                return true;
            }
        }

        [HttpGet("{id}/GetChecklistProgress")]
        public int GetChecklistProgress(int id)
        {
            var count = 0;
            var guests = _guestRepository.AllGuestsByEventId(id);
            if (guests.Any())
            {
                count++;
            }
            var budgetStatus = _budgetRepository.CheckStatus(id);
            if (budgetStatus)
            {
                count++;
            }

            if (_eventRepository.CheckDateAndTime(id))
            {
                count++;
            }
            return count;
        }

        [HttpPost("{id}/SaveDate")]
        public IActionResult SaveDate(int id, [FromBody] Dictionary<string, string> dateInfo)
        {
            var correctDateAndTime = _eventRepository.SaveEventDateAndTime(id, dateInfo);
            if (correctDateAndTime)
            {
                return Ok(correctDateAndTime);
            }
            else return BadRequest(correctDateAndTime);

        }

        [HttpGet("{id}/GetEventStartDate")]
        public IActionResult GetEventStartDate(int id)
        {
            var eventStartDate = _eventRepository.GetEventStartDate(id);
            var test = eventStartDate.ToString("yyyy-MM-dd'T'HH:mm:ss");
            var test2 = eventStartDate.ToString("yyyy-MM-dd'T'HH:mm:ss.fffffff'Z'");
            // DateTimeOffset dto = new DateTimeOffset(eventStartDate);
            // DateTimeOffset timeNow = new DateTimeOffset(DateTime.Now);
            // TimeSpan ts = new TimeSpan(1, 12,
            //     15, 16);
            // DateTimeOffset value = timeNow.Subtract(ts);
            // var unixTimeNow = timeNow.ToUnixTimeMilliseconds();
            // var unixTime = dto.ToUnixTimeMilliseconds();
            // var timeToEvent = unixTime - unixTimeNow;
            // DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(unixTime);
            return Ok(test);
        }

        [HttpGet("{id}/GetEventStatus")]
        public string GetEventStatus(int id)
        {
            return _eventRepository.GetStatus(id);

        }

        [HttpGet("{id}/GetEventInfo")]
        public Dictionary<string, string> GetEventInfo(int id)
        {
            return _eventRepository.GetInfo(id);

        }

    }
}
