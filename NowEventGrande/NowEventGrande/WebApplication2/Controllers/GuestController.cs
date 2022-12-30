using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Data;
using WebApplication2.Models;
using WebApplication2.Services.VerificationService;

namespace WebApplication2.Controllers
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
    }
}
