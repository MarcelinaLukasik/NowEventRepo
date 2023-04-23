using Microsoft.EntityFrameworkCore;
using NowEvent.Data;
using NowEvent.Data.Repositories.BudgetRepository;
using NowEvent.Data.Repositories.EventRepository;
using NowEvent.Data.Repositories.LocationAndTimeRepository;
using NowEvent.Models;

namespace NowEvent.Test
{
    [TestClass]
    public class EventUnitTest
    {
        private readonly ILocationAndTimeRepository _locationRepository = null!;
        private readonly IBudgetRepository _budgetRepository = null!;

        [TestMethod]
        public void TestGetStatus()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "NowEvent")
                .Options;
            Event testEvent = new Event
            {
                Size = "Large",
                Type = "Festival",
                Name = "FestivalEvent",
                Status = "Incomplete"
            };
            using var context = new AppDbContext(options);
            context.Events.Add(testEvent);
            context.SaveChanges();
            EventRepository eventRepository = new EventRepository(context, _budgetRepository, _locationRepository);
            var status = eventRepository.GetStatus(testEvent.Id).Result;

            Assert.AreEqual("Incomplete", status);
        }

        [TestMethod]
        public void TestGetEventStartDate()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "NowEvent")
                .Options;
            DateTime dateToCheck = new DateTime(2023, 12 , 23 ,06,00,00);
            Event testEvent = new Event
            {
                Size = "Large",
                Type = "Festival",
                Name = "FestivalEvent",
                Status = "Incomplete",
                EventStart = dateToCheck
            };
            using var context = new AppDbContext(options);
            context.Events.Add(testEvent);
            context.SaveChanges();
            EventRepository eventRepository = new EventRepository(context, _budgetRepository, _locationRepository);
            var dateResult = eventRepository.GetEventStartDate(testEvent.Id);

            Assert.AreEqual(dateToCheck, dateResult);
        }

        [TestMethod]
        public void TestSetEventDateAndTime()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "NowEvent")
                .Options;
            DateTime eventStart = new DateTime(2023, 12, 23, 06, 00, 00);
            DateTime eventEnd = new DateTime(2023, 12, 23, 09, 00, 00);
            DateTime eventDate = new DateTime(2023, 12, 23, 06, 00, 00);
            Dictionary<string, string> formattedInfo = new Dictionary<string, string>()
            {
                ["Date"] = "2022 - 01 - 19T23:00:00.000Z",
                ["StartTime"] = "04:00 AM",
                ["EndTime"] = "08:00 AM"
            };
            Event testEvent = new Event
            {
                Size = "Large",
                Type = "Festival",
                Name = "FestivalEvent",
                Status = "Incomplete",
                EventStart = eventStart,
                EventEnd = eventEnd,
                Date = eventDate
            };
            using var context = new AppDbContext(options);
            context.Events.Add(testEvent);
            context.SaveChanges();
            EventRepository eventRepository = new EventRepository(context, _budgetRepository, _locationRepository);
            var date = eventRepository.SetEventDateAndTime(testEvent.Id, formattedInfo);

            Assert.IsFalse(date.Result);
        }
    }
}