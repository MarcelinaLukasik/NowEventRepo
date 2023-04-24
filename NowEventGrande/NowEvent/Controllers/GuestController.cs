using Microsoft.AspNetCore.Mvc;
using NowEvent.Data;
using NowEvent.Data.Repositories.GuestRepository;
using NowEvent.Models;
using NowEvent.Services.VerificationService;

namespace NowEvent.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GuestController : ControllerBase
    {
        private readonly IGuestRepository _guestRepository;
        private readonly IVerificationService _verificationService;
        public GuestController(IGuestRepository guestRepository, IVerificationService verificationService)
        {
            _guestRepository = guestRepository;
            _verificationService = verificationService;
        }

        [HttpPost("SaveGuest")]
        public IActionResult SaveGuest([FromBody] Guest guest)
        {
            bool isGuestValid = _verificationService.VerifyGuest(guest);
            if (isGuestValid)
            {
                _guestRepository.AddGuest(guest);
                return Ok(guest);
            }
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
            bool isRemoved = _guestRepository.RemoveGuest(id);
            if (isRemoved)
                return Ok(id);
            return BadRequest(id);
        }
    }
}
