using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
