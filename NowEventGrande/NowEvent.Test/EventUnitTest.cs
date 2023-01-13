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
        public void Test_GetStatus()
        {
            var mockSet = new Mock<DbSet<Event>>();
            var events = new List<Event>() {
                new Event() {
                    Id = 9999,
                    Size = "Large",
                    Type = "Festival",
                    Name = "FestivalEvent",
                    Status = "Incomplete"

                }
            };
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "NowEvent")
                .Options;
            var queryable = events.AsQueryable();
            mockSet.As<IQueryable<Event>>().Setup(m => m.Expression).Returns(queryable.Expression);
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
        public void Test_GetEventStartDate()
        {

        }
    }
}