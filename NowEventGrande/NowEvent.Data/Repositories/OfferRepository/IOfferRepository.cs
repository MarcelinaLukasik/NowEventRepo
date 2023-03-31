using NowEvent.Models;

namespace NowEvent.Data;

public interface IOfferRepository
{
    Task<IEnumerable<Offer>> GetAllOffers();
    Task<Offer> GetOfferById(int id);
    Task AddOffer(Offer offer);
    IQueryable GetOffersByUserId(string id);
    string GetClientIdByEventId(int id);
    Dictionary<string, string> GetDetails(int id);
    Offer GetOfferByEventId(int id);
}