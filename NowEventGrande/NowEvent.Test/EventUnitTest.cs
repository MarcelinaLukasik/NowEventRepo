using Microsoft.EntityFrameworkCore;
using Moq;
using NowEvent.Data;
using NowEvent.Models;
using NowEvent.Services.DateAndTimeService;
using NowEvent.Services.VerificationService;

namespace NowEvent.Test
{
    [TestClass]
    public class EventUnitTest
    {
        private readonly ILocationAndTimeRepository _locationRepository;
        private readonly IBudgetRepository _budgetRepository;

        [TestMethod]
        public void TestGetStatus()
        {
            // var mockSet = new Mock<DbSet<Event>>();
            // var events = new List<Event>() {
            //     new Event() {
            //         Id = 9999,
            //         Size = "Large",
            //         Type = "Festival",
            //         Name = "FestivalEvent",
            //         Status = "Incomplete"
            //
            //     }
            // };

            // var queryable = events.AsQueryable();
            // mockSet.As<IQueryable<Event>>().Setup(m => m.Expression).Returns(queryable.Expression);
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "NowEvent")
                .Options;
            using (var context = new AppDbContext(options))
            {
                context.Events.Add(new Event
                {
                    Id = 9999,
                    Size = "Large",
                    Type = "Festival",
                    Name = "FestivalEvent",
                    Status = "Incomplete"

                });

                context.SaveChanges();
            }
            using (var context = new AppDbContext(options))
            {
                EventRepository eventRepository = new EventRepository(context, _budgetRepository, _locationRepository);
                var status = eventRepository.GetStatus(9999);
                Assert.AreEqual("Incomplete", status.Result);
            }
        }

        [TestMethod]
        public void TestGetEventStartDate()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "NowEvent")
                .Options;
            DateTime dateTocheck = new DateTime(2023, 12 , 23 ,06,00,00);
            using (var context = new AppDbContext(options))
            {
                context.Events.Add(new Event
                {
                    Id = 99999,
                    Size = "Large",
                    Type = "Festival",
                    Name = "FestivalEvent",
                    Status = "Incomplete",
                    EventStart = dateTocheck

                });

                context.SaveChanges();
            }
            using (var context = new AppDbContext(options))
            {
                EventRepository eventRepository = new EventRepository(context, _budgetRepository, _locationRepository);
                var dateResult = eventRepository.GetEventStartDate(99999);
                Assert.AreEqual(dateTocheck, dateResult);
            }
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
            using (var context = new AppDbContext(options))
            {
                context.Events.Add(new Event
                {
                    Id = 999999,
                    Size = "Large",
                    Type = "Festival",
                    Name = "FestivalEvent",
                    Status = "Incomplete",
                    EventStart = eventStart,
                    EventEnd = eventEnd,
                    Date = eventDate

                });

                context.SaveChanges();
            }
            using (var context = new AppDbContext(options))
            {
                EventRepository eventRepository = new EventRepository(context, _budgetRepository, _locationRepository);
                var date = eventRepository.SetEventDateAndTime(999999, formattedInfo);
                Assert.IsFalse(date.Result);
            }
        }
    }
}