using WebApplication2.Models;

namespace WebApplication2.Data;

public interface IOfferRepository
{
    IEnumerable<Offer> GetAllOffers();
    Offer GetOfferById(int id);
    void AddOffer(Offer offer);
}