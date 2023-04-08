using Microsoft.EntityFrameworkCore;
using NowEvent.Models;
using NowEvent.Models.Constants;

namespace NowEvent.Data.Repositories.OfferRepository
{
    public class OfferRepository : IOfferRepository
    {
        private readonly AppDbContext _appDbContext;
        private Dictionary<string, string> _offerDetails = new ();

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

        public string GetClientIdByEventId(int id)
        {
            var clientId = _appDbContext.Events.Where(evt => evt.Id == id)
                .Select(evt => evt.ClientId).FirstOrDefault();
            return clientId;
        }

        public Offer GetOfferByEventId(int id)
        {
            var offerByEventId = _appDbContext.Offer.FirstOrDefault(evt => evt.EventId == id);
            return offerByEventId;
        }

        public Dictionary<string, string> GetDetails(int id)
        {
            var mainEventInfo = _appDbContext.Events.FirstOrDefault(evt => evt.Id == id);
            var eventAddress = _appDbContext.EventAddress.FirstOrDefault(evt => evt.EventId == id);
            var eventBudget = _appDbContext.Budget.FirstOrDefault(evt => evt.EventId == id);
            bool isInfoProvided = mainEventInfo !=null && eventAddress !=null && eventBudget !=null;
            if (isInfoProvided)
            {
                _offerDetails.Add(EventInfoFields.Name, mainEventInfo.Name);
                _offerDetails.Add(EventInfoFields.Type, mainEventInfo.Type);
                _offerDetails.Add(Date.EventStart, mainEventInfo.EventStart.ToShortDateString());
                _offerDetails.Add(EventInfoFields.Theme, mainEventInfo.Theme ?? "-");
                _offerDetails.Add(EventInfoFields.Size, mainEventInfo.Size);
                _offerDetails.Add(EventInfoFields.Address, eventAddress.FullAddress);
                _offerDetails.Add(EventInfoFields.Budget, eventBudget.Total.ToString());
            }
            
            return _offerDetails;
        }
    }
}
