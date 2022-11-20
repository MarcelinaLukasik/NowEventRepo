
using WebApplication2.Models;

namespace WebApplication2.Data
{
    public interface IGuestRepository
    {
        IEnumerable<Guest> AllGuests { get; }
        Guest GetGuestById(int id);
        void AddGuest(Guest guest);

        void RemoveGuest(int id);
        public IEnumerable<Guest> SortDescending();
        public IEnumerable<Guest> SortAscending();

        IEnumerable<Guest> AllGuestsByEventId(int eventId);
    }
}
