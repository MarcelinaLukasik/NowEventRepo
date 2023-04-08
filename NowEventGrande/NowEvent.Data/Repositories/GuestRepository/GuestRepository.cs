using NowEvent.Models;

namespace NowEvent.Data.Repositories.GuestRepository
{
    public class GuestRepository : IGuestRepository
    {
        private readonly AppDbContext _appDbContext;

        public GuestRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Guest> AllGuests => _appDbContext.Guests;

        public IEnumerable<Guest> AllGuestsByEventId(int eventId)
        {
            return _appDbContext.Guests.Where(guests => guests.EventId == eventId);
        }

        public void AddGuest(Guest guest)
        {
            _appDbContext.Guests.Add(guest);
            _appDbContext.SaveChanges();
        }

        public bool RemoveGuest(int id)
        {
            var guestToRemove = _appDbContext.Guests.FirstOrDefault(g => g.Id == id);
            if (guestToRemove != null)
            {
                _appDbContext.Guests.Remove(guestToRemove);
                _appDbContext.SaveChanges();
                return true;
            }
            return false;

        }

        public IEnumerable<Guest> SortDescending()
        {
            return AllGuests.OrderByDescending(guests => guests.FirstName);
        }

        public IEnumerable<Guest> SortAscending()
        {
            return AllGuests.OrderBy(guests => guests.FirstName);
        }

    }
}
