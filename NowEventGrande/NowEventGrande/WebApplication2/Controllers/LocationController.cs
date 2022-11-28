using Microsoft.AspNetCore.Mvc;
using WebApplication2.Data;
using WebApplication2.Models;
using WebApplication2.Services.VerificationService;

namespace WebApplication2.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationRepository _locationRepository;
        private readonly IVerificationService _verificationService;

        public LocationController(ILocationRepository locationRepository, IVerificationService verificationService)
        {
            _locationRepository = locationRepository;
            _verificationService = verificationService;
        }

        [HttpGet("{id}/GetLocation")]
        public IActionResult GetGuest(int id)
        {
            var location = _locationRepository.GetLocation(id);
            return Ok(location);
        }

        [HttpGet("GetMapsKey")]
        public string GetMapsKey()
        {
            var keys = System.IO.File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Key.txt"));
            var mapKey = keys.Split(";")[1].Trim();
            return mapKey;

        }

        [HttpPost("SaveLocation")]
        public IActionResult SaveLocation(EventAddress eventAddress)
        {
            _locationRepository.SaveLocation(eventAddress);
            return Ok(eventAddress);
        }

        [HttpGet("{id}/GetVerificationInfo")]
        public Dictionary<string, string> GetVerificationInfo(int id)
        {
            return _verificationService.GetVerificationInfo(id);

        }



    }
}
