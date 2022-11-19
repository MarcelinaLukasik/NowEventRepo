using Microsoft.AspNetCore.Mvc;
using WebApplication2.Data;

namespace WebApplication2.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationRepository _locationRepository;

        public LocationController(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        [HttpGet("{id}/GetLocation")]
        public IActionResult GetGuest(int id)
        {
            var location = _locationRepository.GetLocation(id);
            return Ok(location);
        }

   

    }
}
