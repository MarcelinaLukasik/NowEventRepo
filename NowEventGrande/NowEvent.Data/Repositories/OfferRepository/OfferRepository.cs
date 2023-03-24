using Microsoft.EntityFrameworkCore;
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

        public async Task AddOffer(Offer offer)
        {
            await _appDbContext.Offer.AddAsync(offer);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task<IEnumerable<Offer>> GetAllOffers()
        {
            return await _appDbContext.Offer.ToListAsync();
        }

        public async Task<Offer> GetOfferById(int id)
        {
            return await _appDbContext.Offer.FindAsync(id);
        }

        public IQueryable GetOffersByUserId(string id)
        {
            var result = _appDbContext.Offer.Join(_appDbContext.Events, 
                    offer => offer.EventId, evt => evt.Id,
                (offer, evt) => new { Offer = offer, Evt = evt})
                .Where(offerAndEvt => offerAndEvt.Evt.ClientId == id)
                .Select(o => new { o.Evt.Name, o.Offer.Status}); 
            return result;
        }
    }
}
