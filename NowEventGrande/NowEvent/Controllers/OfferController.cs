using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NowEvent.Data;
using NowEvent.Models;

namespace NowEvent.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OfferController : ControllerBase
    {
        private readonly ILogger<OfferController> _logger;
        private readonly IOfferRepository _offerRepository;
        private readonly IEventRepository _eventRepository;

        public OfferController(ILogger<OfferController> logger, IOfferRepository offerRepository, IEventRepository eventRepository)
        {
            _logger = logger;
            _offerRepository = offerRepository;
            _eventRepository = eventRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Event>>> GetAll([FromQuery]OfferQuery query)
        {
            var offers = await _eventRepository.GetAll(query);
            return Ok(offers);
        }

        [HttpGet("singleOffer/{id}")]
        public async Task<Event> GetByIdAsync(int id)
        {
            return await _eventRepository.GetEventById(id);
        }

        [HttpPost("PostOffer")]
        public async Task<IActionResult> AddOffer([FromBody] Offer offer)
        {
            await _offerRepository.AddOffer(offer);
            return Ok(offer);
        }


    }
}
