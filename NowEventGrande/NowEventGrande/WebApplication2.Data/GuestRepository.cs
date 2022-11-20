using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace WebApplication2.Data
{
    public class GuestRepository : IGuestRepository
    {
        private readonly AppDbContext _appDbContext;

        public GuestRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Guest> AllGuests
        {
            get
            {
                return _appDbContext.Guests;
            }
        }

        public IEnumerable<Guest> AllGuestsByEventId(int eventId)
        {
            return _appDbContext.Guests.Where(guests => guests.EventId == eventId);
        }

        public Guest GetGuestById(int id)
        {
            return _appDbContext.Guests.First(p => p.Id == id);
        }

        public void AddGuest(Guest guest)
        {
            _appDbContext.Guests.Add(guest);
            _appDbContext.SaveChanges();
        }

        public void RemoveGuest(int id)
        {
            var guestToRemove = _appDbContext.Guests.First(p => p.Id == id);
            _appDbContext.Guests.Remove(guestToRemove);
            _appDbContext.SaveChanges();
        }

        public IEnumerable<Guest> SortDescending()
        {
            var guests = AllGuests.OrderByDescending(guests => guests.FirstName);
            return guests;
        }

        public IEnumerable<Guest> SortAscending()
        {
            return AllGuests.OrderBy(guests => guests.FirstName);
        }

    }
}
