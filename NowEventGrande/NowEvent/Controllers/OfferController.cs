using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NowEvent.Data;
using NowEvent.Data.Repositories.RequestsRepository;
using NowEvent.Models;
using NowEvent.Models.Constants;

namespace NowEvent.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OfferController : ControllerBase
    {
        private readonly IOfferRepository _offerRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IRequestRepository _requestRepository;
        public OfferController(IOfferRepository offerRepository,
            IEventRepository eventRepository, IRequestRepository requestRepository)
        {
            _offerRepository = offerRepository;
            _eventRepository = eventRepository;
            _requestRepository = requestRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Event>>> GetAll()
        {
            var offers = await _eventRepository.GetAllOffers();
            return Ok(offers);
        }

        [HttpGet("{id}")]
        public async Task<Event> GetByIdAsync(int id)
        {
            return await _eventRepository.GetEventByIdAsync(id);
        }

        [HttpPost("PostOffer")]
        public async Task<IActionResult> AddOffer([FromBody] Offer offer)
        {
            await _offerRepository.AddOffer(offer);
            await _eventRepository.SetStatus(offer.EventId, EventStatuses.Posted);
            return Ok(offer);
        }

        [HttpPost("GetOffersByUserId")]
        [Authorize]
        public IQueryable GetOffersByUserId([FromBody] string id)
        {
            return _offerRepository.GetOffersByUserId(id);
   
        }

        [HttpGet("{id}/GetClientId")]
        public IActionResult GetClientId(int id)
        {
            string clientId = _offerRepository.GetClientIdByEventId(id);
            return Ok(clientId);
        }

        [HttpPost("PostRequest")]
        public IActionResult PostRequest(Request request)
        {
            _requestRepository.SaveRequest(request);
            return Ok(request);
        }

        [HttpPost("GetRequestsByUserId")]
        [Authorize]
        public IQueryable GetRequestsByUserId([FromBody] string id)
        {
            var result = _requestRepository.GetRequestsByUserId(id);
            return result;
        }

        [HttpGet("{id:int}/GetSingleRequest")]
        public async Task<Request> GetSingleRequest(int id)
        {
            Request request = await _requestRepository.GetRequestById(id);
            return request;
        }

        [HttpGet("{id:int}/GetOfferDetails")]
        public IActionResult GetOfferDetails(int id)
        {
            var offerDetails = _offerRepository.GetDetails(id);
            return Ok(offerDetails);
        }
    }
}
