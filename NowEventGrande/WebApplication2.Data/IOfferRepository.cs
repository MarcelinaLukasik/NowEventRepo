using NowEvent.Models;

namespace NowEvent.Data;

public interface IOfferRepository
{
    IEnumerable<Offer> GetAllOffers();
    Offer GetOfferById(int id);
    void AddOffer(Offer offer);
}