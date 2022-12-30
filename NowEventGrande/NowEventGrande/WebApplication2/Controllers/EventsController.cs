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
using WebApplication2.Services.DateAndTimeService;

namespace WebApplication2.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly ILogger<EventsController> _logger;
        // private readonly IGuestRepository _guestRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IVerificationService _verificationService;
        // private readonly IUserAuthenticationService _userAuthenticationService;
        private readonly IDateAndTimeService _dateAndTimeService;


        public EventsController(ILogger<EventsController> logger, IEventRepository eventRepository, 
            IVerificationService verificationService, IDateAndTimeService dateAndTimeService)
        {
            _logger = logger;
            // _guestRepository = guestRepository;
            _eventRepository = eventRepository;
            _verificationService = verificationService;
            // _userAuthenticationService = userAuthenticationService;
            _dateAndTimeService = dateAndTimeService;
        }

        //
        // [HttpPost("SaveGuest")]
        // public IActionResult SaveGuest([FromBody] Guest guest)
        // {
        //     bool validGuest = _verificationService.VerifyGuest(guest);
        //     if (validGuest)
        //     {
        //         _guestRepository.AddGuest(guest);
        //         return Ok(guest);
        //     }
        //     else
        //         return BadRequest(guest);
        // }

        // [HttpGet("{id:int}/all")]
        // public IEnumerable<Guest> GetAllGuests(int id)
        // {
        //     return _guestRepository.AllGuestsByEventId(id);
        // }
        //
        // [HttpGet("{id:int}/all/descending")]
        // public IEnumerable<Guest> GuestsSortedDescending(int id)
        // {
        //     return _guestRepository.SortDescending().Where(x => x.EventId == id).ToArray();
        // }
        //
        // [HttpGet("{id:int}/all/ascending")]
        // public IEnumerable<Guest> GuestsSortedAscending(int id)
        // {
        //     return _guestRepository.SortAscending().Where(x => x.EventId == id).ToArray();
        // }
        //
        // [HttpDelete("removeGuest/{id:int}")]
        // public IActionResult RemoveGuest(int id)
        // {
        //     _guestRepository.RemoveGuest(id);
        //     return Ok(id);
        // }

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

        [HttpPost("SaveRatings")]
        public IActionResult SaveRatings([FromBody] Rating rating)
        {
            //TODO save rating to database
            return Ok(rating);
        }


        [HttpGet("GetKey")]
        public string GetKey()
        {
            return (System.IO.File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Key.txt"))).Split(";")[0];
        }



        [HttpPost("{id:int}/SaveDate")]
        public IActionResult SaveDate(int id, [FromBody] Dictionary<string, string> dateInfo)
        {
            Dictionary<string, string> formattedDateInfo = _dateAndTimeService.FormatDateInfo(dateInfo);
            return _eventRepository.SetEventDateAndTime(id, formattedDateInfo) ? Ok() : BadRequest();
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

        [HttpGet("{id:int}/CheckIfLargeSize")]
        public bool CheckIfLargeSize(int id)
        {
            return _eventRepository.CheckIfLargeSize(id);
           
        }



    }
}
