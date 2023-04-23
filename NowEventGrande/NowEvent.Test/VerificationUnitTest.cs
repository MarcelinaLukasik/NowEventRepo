using Microsoft.EntityFrameworkCore;
using NowEvent.Data;
using NowEvent.Data.Repositories.BudgetRepository;
using NowEvent.Data.Repositories.EventRepository;
using NowEvent.Data.Repositories.LocationAndTimeRepository;
using NowEvent.Models;
using NowEvent.Models.Constants;
using NowEvent.Services.BudgetService;
using NowEvent.Services.DateAndTimeService;
using NowEvent.Services.VerificationService;

namespace NowEvent.Test
{
    [TestClass]
    public class VerificationUnitTest
    {

        private readonly ILocationAndTimeRepository _locationRepository = null!;
        private readonly IEventRepository _eventRepository = null!;
        private readonly IBudgetService _budgetService = null!;
        private readonly IBudgetRepository _budgetRepository = null!;
        private readonly IDateAndTimeService _dateAndTimeService = null!;

        [TestMethod]
        public void Test_IncorrectLastNameInGuestInfo()
        {
            VerificationService verificationService = new VerificationService(_locationRepository,
                _eventRepository, _budgetService, _dateAndTimeService);
            Guest guest = new Guest { FirstName = "John", LastName = "123", Email = "doe@gmail.com" };
            bool result = verificationService.VerifyGuest(guest);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_CorrectGuestInfo()
        {
            VerificationService verificationService = new VerificationService(_locationRepository,
                _eventRepository, _budgetService, _dateAndTimeService);
            Guest guest = new Guest { FirstName = "John", LastName = "Doe", Email = "doe@gmail.com" };
            bool result = verificationService.VerifyGuest(guest);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Test_VerifyCorrectEventData()
        {
            VerificationService verificationService = new VerificationService(_locationRepository,
                _eventRepository, _budgetService, _dateAndTimeService);
            Event newEvent = new Event
                { Type = "Birthday", Size = "Small", Name = "birthdayEvent", Status = "" };
            bool validEvent = verificationService.VerifyEvent(newEvent);
            
            Assert.IsTrue(validEvent);
        }

        [TestMethod]
        public void Test_VerifyEventNameWithNumbers()
        {
            VerificationService verificationService = new VerificationService(_locationRepository,
                _eventRepository, _budgetService, _dateAndTimeService);
            Event newEvent = new Event
                { Type = "Birthday", Size = "Small", Name = "event123", Status = "" };
            bool validEvent = verificationService.VerifyEvent(newEvent);

            Assert.IsFalse(validEvent);
        }

        [TestMethod]
        public void Test_VerifyEmptyBudgetType()
        {
            VerificationService verificationService = new VerificationService(_locationRepository,
                _eventRepository, _budgetService, _dateAndTimeService);
            Event newEvent = new Event
                { Type = "", Size = "Small", Name = "birthdayEvent", Status = "" };
            bool validEvent = verificationService.VerifyEvent(newEvent);

            Assert.IsFalse(validEvent);
        }

        [TestMethod]
        public void Test_VerifyCorrectBudgetPrices()
        {
            VerificationService verificationService = new VerificationService(_locationRepository,
                _eventRepository, _budgetService, _dateAndTimeService);
            bool validPrices = verificationService.VerifyBudgetPrice("123");
            Assert.IsTrue(validPrices);
        }

        [TestMethod]
        public void Test_IncorrectCharsInBudgetPrices()
        {
            VerificationService verificationService = new VerificationService(_locationRepository,
                _eventRepository, _budgetService, _dateAndTimeService);
            bool validPrices = verificationService.VerifyBudgetPrice("!#$%");
            Assert.IsFalse(validPrices);
        }

        [TestMethod]
        public void Test_FormatStartTimeAM()
        {
            DateAndTimeService dateAndTimeService = new DateAndTimeService(_eventRepository);
            Dictionary<string, string> dictWithTime = new Dictionary<string, string>
            {
                { "StartHour", "1" }, {"StartMinutes", "20"}, {"TimeOfDayStart", "AM"},
                { "EndHour", "2" }, {"EndMinutes", "30"}, {"TimeOfDayEnd", "AM"}
            };
            Dictionary<string, string> result = dateAndTimeService.FormatDateInfo(dictWithTime);
            
            Assert.AreEqual("1:20 AM", result["StartTime"]);

        }

        [TestMethod]
        public void Test_FormatEndTimePM()
        {
            DateAndTimeService dateAndTimeService = new DateAndTimeService(_eventRepository);
            Dictionary<string, string> dictWithTime = new Dictionary<string, string>()
            {
                { "StartHour", "7" }, {"StartMinutes", "20"}, {"TimeOfDayStart", "PM"},
                { "EndHour", "5" }, {"EndMinutes", "00"}, {"TimeOfDayEnd", "PM"}
            };
            Dictionary<string, string> result = dateAndTimeService.FormatDateInfo(dictWithTime);

            Assert.AreEqual("7:20 PM", result["StartTime"]);
        }

        [TestMethod]
        public void Test_GetOperationalHoursStart()
        {
            DateAndTimeService dateAndTimeService = new DateAndTimeService(_eventRepository);
            string dayInfo = "10:00–05:00";
            DateTime date = new DateTime(2023, 12, 31, 5, 10, 00);
            DateTime result = dateAndTimeService.GetOperationalHour(dayInfo, EventTimeStages.Start, date);
            DateTime expectedDate = new DateTime(2023, 12, 31, 10, 00, 00);

            Assert.AreEqual(expectedDate, result);
        }
        [TestMethod]
        public void Test_GetOperationalHoursEnd()
        {
            DateAndTimeService dateAndTimeService = new DateAndTimeService(_eventRepository);
            string dayInfo = "10:00–05:00";
            DateTime date = new DateTime(2023, 12, 31, 5, 10, 00);
            DateTime result = dateAndTimeService.GetOperationalHour(dayInfo, EventTimeStages.End, date);
            DateTime expectedDate = new DateTime(2023, 12, 31, 05, 00, 00);

            Assert.AreEqual(expectedDate, result);
        }
 
        [TestMethod]
        public void Test_GetStatus()
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
            var status = eventRepository.GetStatus(testEvent.Id);

            Assert.AreEqual("Incomplete", status.Result);
        }

    }
}