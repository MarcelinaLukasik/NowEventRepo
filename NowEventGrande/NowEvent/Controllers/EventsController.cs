using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NowEvent.Data;
using NowEvent.Models;
using NowEvent.Services.DateAndTimeService;
using NowEvent.Services.VerificationService;
using EventData = NowEvent.Data.EventData;

namespace NowEvent.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly ILogger<EventsController> _logger;
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
        public async Task<IActionResult> SaveDate(int id, [FromBody] Dictionary<string, string> dateInfo)
        {
            Dictionary<string, string> formattedDateInfo = _dateAndTimeService.FormatDateInfo(dateInfo);
            return await _eventRepository.SetEventDateAndTime(id, formattedDateInfo) ? Ok() : BadRequest();
        }

        [HttpGet("{id:int}/GetEventStartDate")]
        public IActionResult GetEventStartDate(int id)
        {
            var getEvent = _eventRepository.GetEventStartDate(id);
            return Ok();
        }

        [HttpGet("{id:int}/GetEventStatus")]
        public async Task<string> GetEventStatus(int id)
        {
            return await _eventRepository.GetStatus(id);
        }

        [HttpGet("{id:int}/GetEventInfo")]
        public async Task<Dictionary<string, string>> GetEventInfo(int id)
        {
            return await _eventRepository.GetInfo(id);
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
