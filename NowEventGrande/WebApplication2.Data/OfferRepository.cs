using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NowEvent.Models;

namespace NowEvent.Data
{
    public class OfferRepository : IOfferRepository
    {
        private readonly AppDbContext _appDbContext;

        public OfferRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void AddOffer(Offer offer)
        {
            _appDbContext.Offer.Add(offer);
            _appDbContext.SaveChanges();
        }
        public IEnumerable<Offer> GetAllOffers()
        {
            return _appDbContext.Offer;
        }

        public Offer GetOfferById(int id)
        {
            return _appDbContext.Offer.FirstOrDefault(o => o.Id == id);
        }
    }
}
