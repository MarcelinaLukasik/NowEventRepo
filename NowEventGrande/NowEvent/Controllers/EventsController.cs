using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NowEvent.Data;
using NowEvent.Data.Repositories.RatingsRepository;
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
        private readonly IRatingsRepository _ratingsRepository;
        private readonly IVerificationService _verificationService;
        private readonly IDateAndTimeService _dateAndTimeService;


        public EventsController(ILogger<EventsController> logger, IEventRepository eventRepository, 
            IVerificationService verificationService, IDateAndTimeService dateAndTimeService,
            IRatingsRepository ratingsRepository)
        {
            _logger = logger;
            _eventRepository = eventRepository;
            _verificationService = verificationService;
            _dateAndTimeService = dateAndTimeService;
            _ratingsRepository = ratingsRepository;
        }


        [HttpPost("CreateNewEvent")]
        [Authorize]
        public IActionResult CreateNewEvent([FromBody] Event newEvent)
        {
            bool isEventValid = _verificationService.VerifyEvent(newEvent);
            int id = 0;
            if (isEventValid)
                id = _eventRepository.AddEvent(newEvent);

            return isEventValid ? Ok(id) : BadRequest(newEvent);
        }
        
        [HttpGet("{eventId:int}/CheckIfRated")]
        public IActionResult CheckIfRated(int eventId)
        {
            bool isRated = _ratingsRepository.RatingStatus(eventId);
            if (isRated)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost("SaveRatings")]
        public IActionResult SaveRatings([FromBody] Rating rating)
        {
            _ratingsRepository.SaveRatings(rating);
            return Ok(rating);
        }


        [HttpGet("GetKey")]
        public string GetKey()
        {
            return (System.IO.File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Key.txt")))
                .Split(";")[0];
        }



        [HttpPost("{id:int}/SaveDate")]
        public async Task<IActionResult> SaveDate(int id, [FromBody] Dictionary<string, string> dateInfo)
        {
            Dictionary<string, string> formattedDateInfo = _dateAndTimeService.FormatDateInfo(dateInfo);
            return await _eventRepository.SetEventDateAndTime(id, formattedDateInfo) ? Ok() : BadRequest();
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
            bool isThemeCorrect = _verificationService.VerifyTheme(theme);
            bool isIdCorrect = _eventRepository.ManageEventData(id, theme, EventData.Theme);
            bool isDataCorrect = isThemeCorrect && isIdCorrect;
            return isDataCorrect ? Ok(isDataCorrect) : BadRequest(isDataCorrect);
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

        [HttpGet("{id:int}/GetEventStartDate")]
        public DateTime GetEventStartDate(int id)
        {
            return _eventRepository.GetEventStartDate(id);
        }
    }
}
