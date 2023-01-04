using WebApplication2.Data;
using WebApplication2.Models;
using WebApplication2.Services.DateAndTimeService;
using WebApplication2.Services.VerificationService;

namespace NewEventTest
{
    [TestClass]
    public class VerificationUnitTest
    {

        private readonly ILocationAndTimeRepository _locationRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IBudgetRepository _budgetRepository;
        private readonly IDateAndTimeService _dateAndTimeService;

        [TestMethod]
        public void Test_VerifyGuestAllowedChars()
        {
            VerificationService verificationService = new VerificationService(_locationRepository,
                _eventRepository, _budgetRepository, _dateAndTimeService);
            Guest guest = new Guest() { FirstName = "John", LastName = "123"};
            Guest guest2 = new Guest() { FirstName = "John", LastName = "Doe" };
            bool result = verificationService.VerifyGuestName(guest);
            bool result2 = verificationService.VerifyGuestName(guest2);
            Assert.IsFalse(result);
            Assert.IsTrue(result2);
        }

        [TestMethod]
        public void Test_VerifyCorrectEventData()
        {
            VerificationService verificationService = new VerificationService(_locationRepository,
                _eventRepository, _budgetRepository, _dateAndTimeService);
            Event newEvent = new Event
                { Id = 99999, Type = "Birthday", Size = "Small", Name = "birthdayEvent", Status = "" };
            bool validEvent = verificationService.VerifyEvent(newEvent);
            
            Assert.IsTrue(validEvent);
        }

        [TestMethod]
        public void Test_VerifyEventNameWithNumbers()
        {
            VerificationService verificationService = new VerificationService(_locationRepository,
                _eventRepository, _budgetRepository, _dateAndTimeService);
            Event newEvent = new Event
                { Id = 99999, Type = "Birthday", Size = "Small", Name = "event123", Status = "" };
            bool validEvent = verificationService.VerifyEvent(newEvent);

            Assert.IsFalse(validEvent);
        }

        [TestMethod]
        public void Test_VerifyEmptyBudgetType()
        {
            VerificationService verificationService = new VerificationService(_locationRepository,
                _eventRepository, _budgetRepository, _dateAndTimeService);
            Event newEvent = new Event
                { Id = 99999, Type = "", Size = "Small", Name = "birthdayEvent", Status = "" };
            bool validEvent = verificationService.VerifyEvent(newEvent);

            Assert.IsFalse(validEvent);
        }

        [TestMethod]
        public void Test_BudgetPrices()
        {
            VerificationService verificationService = new VerificationService(_locationRepository,
                _eventRepository, _budgetRepository, _dateAndTimeService);
            bool validPrices = verificationService.VerifyBudgetPrice("123");
            bool validPrices2 = verificationService.VerifyBudgetPrice("!#$%");
            Assert.IsTrue(validPrices);
            Assert.IsFalse(validPrices2);
        }

        [TestMethod]
        public void Test_FormatStartTimeAM()
        {
            DateAndTimeService dateAndTimeService = new DateAndTimeService();
            Dictionary<string, string> dictWithTime = new Dictionary<string, string>()
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
            DateAndTimeService dateAndTimeService = new DateAndTimeService();
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
            DateAndTimeService dateAndTimeService = new DateAndTimeService();
            string dayInfo = "10:00–05:00";
            DateTime date = new DateTime(2023, 12, 31, 5, 10, 00);
            DateTime result = dateAndTimeService.GetOperationalHour(dayInfo, EventTimeStages.Start, date);
            DateTime expectedDate = new DateTime(2023, 12, 31, 10, 00, 00);

            Assert.AreEqual(expectedDate, result);
        }
        [TestMethod]
        public void Test_GetOperationalHoursEnd()
        {
            DateAndTimeService dateAndTimeService = new DateAndTimeService();
            string dayInfo = "10:00–05:00";
            DateTime date = new DateTime(2023, 12, 31, 5, 10, 00);
            DateTime result = dateAndTimeService.GetOperationalHour(dayInfo, EventTimeStages.End, date);
            DateTime expectedDate = new DateTime(2023, 12, 31, 05, 00, 00);

            Assert.AreEqual(expectedDate, result);
        }



    }
}