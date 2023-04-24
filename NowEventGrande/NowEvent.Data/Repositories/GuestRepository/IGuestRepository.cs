﻿using NowEvent.Models;

namespace NowEvent.Data.Repositories.GuestRepository
{
    public interface IGuestRepository
    {
        void AddGuest(Guest guest);
        bool RemoveGuest(int id);
        public IEnumerable<Guest> SortDescending();
        public IEnumerable<Guest> SortAscending();
        IEnumerable<Guest> AllGuestsByEventId(int eventId);
    }
}
