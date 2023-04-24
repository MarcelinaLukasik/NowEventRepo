using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NowEvent.Models;
using NowEvent.Models.Constants;
using NowEvent.Services.DateAndTimeService;
using NowEvent.Services.EventService;
using NowEvent.Services.RatingsService;
using NowEvent.Services.VerificationService;

namespace NowEvent.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;
        private readonly IRatingsService _ratingsService;
        private readonly IVerificationService _verificationService;
        private readonly IDateAndTimeService _dateAndTimeService;

        public EventsController(IEventService eventService, 
            IVerificationService verificationService, IDateAndTimeService dateAndTimeService,
            IRatingsService ratingsService)
        {
        
            _eventService = eventService;
            _verificationService = verificationService;
            _dateAndTimeService = dateAndTimeService;
            _ratingsService = ratingsService;
        }

        [HttpPost("CreateNewEvent")]
        [Authorize]
        public IActionResult CreateNewEvent([FromBody] Event newEvent)
        {
            bool isEventValid = _verificationService.VerifyEvent(newEvent);
            int id = 0;
            if (isEventValid)
                id = _eventService.AddEvent(newEvent);

            return isEventValid ? Ok(id) : BadRequest(newEvent);
        }
        
        [HttpGet("{eventId:int}/CheckIfRated")]
        public IActionResult CheckIfRated(int eventId)
        {
            bool isRated = _ratingsService.RatingStatus(eventId);
            if (isRated)
            {
                return Ok();
            }
            return NotFound();
        }

        [HttpPost("SaveRatings")]
        public IActionResult SaveRatings([FromBody] Rating rating)
        {
            _ratingsService.SaveRatings(rating);
            return Ok(rating);
        }

        [HttpGet("GetKey")]
        public string GetKey()
        {
            return System.IO.File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Key.txt"))
                .Split(";")[0];
        }

        [HttpPost("{id:int}/SaveDate")]
        public async Task<IActionResult> SaveDate(int id, [FromBody] Dictionary<string, string> dateInfo)
        {
            Dictionary<string, string> formattedDateInfo = _dateAndTimeService.FormatDateInfo(dateInfo);
            return await _eventService.SetEventDateAndTime(id, formattedDateInfo) ? Ok() : BadRequest();
        }

        [HttpGet("{id:int}/GetEventStatus")]
        public async Task<string> GetEventStatus(int id)
        {
            return await _eventService.GetStatus(id);
        }

        [HttpGet("{id:int}/GetEventInfo")]
        public async Task<Dictionary<string, string>> GetEventInfo(int id)
        {
            return await _eventService.GetInfo(id);
        }

        [HttpPost("{id:int}/SetSize")]
        public IActionResult SetSize(int id, [FromBody] string size)
        {
            bool isDataCorrect = _eventService.ManageEventData(id, size, EventData.Size);
            return isDataCorrect ? Ok(isDataCorrect) : BadRequest(isDataCorrect);
        }


        [HttpPost("{id:int}/SetSizeRange")]
        public IActionResult SetSizeRange(int id, [FromBody] string sizeRange)
        {
            bool isDataCorrect = _eventService.ManageEventData(id, sizeRange, EventData.SizeRange);
            return isDataCorrect ? Ok(isDataCorrect) : BadRequest(isDataCorrect);
        }

        [HttpPost("{id:int}/SetTheme")]
        public IActionResult SetTheme(int id, [FromBody] string theme)
        {
            bool isThemeCorrect = _verificationService.VerifyTheme(theme);
            bool isIdCorrect = _eventService.ManageEventData(id, theme, EventData.Theme);
            bool isDataCorrect = isThemeCorrect && isIdCorrect;
            return isDataCorrect ? Ok(isDataCorrect) : BadRequest(isDataCorrect);
        }

        [HttpPost("GetEventsByUserId")]
        [Authorize]
        public IQueryable GetEventsByUserId([FromBody] string id)
        {
            var result = _eventService.GetEventsByUserId(id);
            return result;
        }
        
        [HttpGet("{id:int}/CheckIfLargeSize")]
        public bool CheckIfLargeSize(int id)
        {
            return _eventService.CheckIfLargeSize(id);
        }

        [HttpGet("{id:int}/GetEventStartDate")]
        public DateTime GetEventStartDate(int id)
        {
            return _eventService.GetEventStartDate(id);
        }
    }
}
