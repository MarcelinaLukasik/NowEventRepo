using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Data;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OffersController : ControllerBase
    {
        private readonly ILogger<OffersController> _logger;
        private readonly IOfferRepository _offerRepository;

        public OffersController(ILogger<OffersController> logger, IOfferRepository offerRepository)
        {
            _logger = logger;
            _offerRepository = offerRepository;
        }

        [HttpGet]
        public IEnumerable<Offer> GetAll()
        {
            return _offerRepository.GetAllOffers().ToList();
        }
    }
}
