using NowEvent.Models;

namespace NowEvent.Data.Repositories.OfferRepository;

public interface IOfferRepository
{
    Task AddOffer(Offer offer);
    IQueryable GetOffersByUserId(string id);
    string GetClientIdByEventId(int id);
    Dictionary<string, string> GetDetails(int id);
    Offer GetOfferByEventId(int id);
}