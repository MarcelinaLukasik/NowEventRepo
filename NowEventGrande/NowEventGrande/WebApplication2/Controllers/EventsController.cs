using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Diagnostics;
using WebApplication2.Data;
using WebApplication2.Models;
using WebApplication2.Services.VerificationService;
using EventData = WebApplication2.Data.EventData;
using WebApplication2.Services.AuthenticationService;

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
        private readonly IVerificationService _verificationService;
        private readonly IUserAuthenticationService _userAuthenticationService;


        public EventsController(ILogger<EventsController> logger, IGuestRepository guestRepository,
            IEventRepository eventRepository, IBudgetRepository budgetRepository,
            IVerificationService verificationService, IOfferRepository offerRepository, 
            IUserAuthenticationService userAuthenticationService)
        {
            _logger = logger;
            _guestRepository = guestRepository;
            _eventRepository = eventRepository;
            _budgetRepository = budgetRepository;
            _offerRepository = offerRepository;
            _verificationService = verificationService;
            _userAuthenticationService = userAuthenticationService;
        }


        [HttpPost("PostOffer")]
        public IActionResult AddOffer([FromBody] Offer offer)
        {
            _offerRepository.AddOffer(offer);
            return Ok(offer);
        }

        [HttpPost("SaveGuest")]
        public IActionResult SaveGuest([FromBody] Guest guest)
        {
            bool validGuest = _verificationService.VerifyGuest(guest);
            if (validGuest)
            {
                _guestRepository.AddGuest(guest);
                return Ok(guest);
            }
            else
                return BadRequest(guest);
        }

        [HttpGet("{id:int}/all")]
        public IEnumerable<Guest> GetAllGuests(int id)
        {
            return _guestRepository.AllGuestsByEventId(id);
        }

        [HttpGet("{id:int}/all/descending")]
        public IEnumerable<Guest> GuestsSortedDescending(int id)
        {
            return _guestRepository.SortDescending().Where(x => x.EventId == id).ToArray();
        }

        [HttpGet("{id:int}/all/ascending")]
        public IEnumerable<Guest> GuestsSortedAscending(int id)
        {
            return _guestRepository.SortAscending().Where(x => x.EventId == id).ToArray();
        }

        [HttpDelete("removeGuest/{id:int}")]
        public IActionResult RemoveGuest(int id)
        {
            _guestRepository.RemoveGuest(id);
            return Ok(id);
        }

        [HttpPost("CreateNewEvent")]
        [Authorize]
        public IActionResult CreateNewEvent([FromBody] Event newEvent)
        {
            bool validEvent = _verificationService.VerifyEvent(newEvent);
            int id = 0;
            if (validEvent)
                id = _eventRepository.AddEvent(newEvent);

            return validEvent ? Ok(id) : BadRequest(newEvent);
        }

        //TODO move to new controller, save to database
        [HttpPost("SaveRatings")]
        public IActionResult SaveRatings([FromBody] Rating rating)
        {
            var test = rating;
            return Ok(test);
        }


        [HttpGet("GetKey")]
        public string GetKey()
        {
            return (System.IO.File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Key.txt"))).Split(";")[0];
        }


        //TODO checkStatus, then setStatus based on checkStatus return value
        [HttpGet("{id:int}/CheckStatus")]
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

            if (!_eventRepository.CheckDateAndTimeByEventId(id))
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

        [HttpGet("{id:int}/GetChecklistProgress")]
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

            if (_eventRepository.CheckDateAndTimeByEventId(id))
            {
                count++;
            }

            return count;
        }

        [HttpPost("{id:int}/SaveDate")]
        public IActionResult SaveDate(int id, [FromBody] Dictionary<string, string> dateInfo)
        {
            return _eventRepository.SetEventDateAndTime(id, dateInfo) ? Ok() : BadRequest();
        }

        [HttpGet("{id:int}/GetEventStartDate")]
        public IActionResult GetEventStartDate(int id)
        {
            return Ok(_eventRepository.GetEventStartDate(id).ToString("yyyy-MM-dd'T'HH:mm:ss"));
        }

        [HttpGet("{id:int}/GetEventStatus")]
        public string GetEventStatus(int id)
        {
            return _eventRepository.GetStatus(id);
        }

        [HttpGet("{id:int}/GetEventInfo")]
        public Dictionary<string, string> GetEventInfo(int id)
        {
            return _eventRepository.GetInfo(id);
        }

        [HttpPost("{id:int}/SetSize")]
        public IActionResult SetSize(int id, [FromBody] string size)
        {
            bool correctData = _eventRepository.ManageEventData(id, size, EventData.Size);
            return correctData ? Ok(correctData) : BadRequest(correctData);
        }


        [HttpPost("{id:int}/SetSizeRange")]
        public IActionResult SetSizeRange(int id, [FromBody] string sizeRange)
        {
            bool correctData = _eventRepository.ManageEventData(id, sizeRange, EventData.SizeRange);
            return correctData ? Ok(correctData) : BadRequest(correctData);
        }

        [HttpPost("{id:int}/SetTheme")]
        public IActionResult SetTheme(int id, [FromBody] string theme)
        {
            bool correctData = _eventRepository.ManageEventData(id, theme, EventData.Theme);
            return correctData ? Ok(correctData) : BadRequest(correctData);
        }


        [HttpPost("GetEventsByUserId")]
        [Authorize]
        public IQueryable GetEventsByUserId([FromBody] string id)
        {
            var result = _eventRepository.GetEventsByUserId(id);
            return result;
        }

        
    }
}
