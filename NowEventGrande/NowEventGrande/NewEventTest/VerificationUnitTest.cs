using WebApplication2.Data;
using WebApplication2.Models;
using WebApplication2.Services.DateAndTimeService;
using WebApplication2.Services.VerificationService;

namespace NewEventTest
{
    [TestClass]
    public class VerificationUnitTest
    {

        private readonly ILocationRepository _locationRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IBudgetRepository _budgetRepository;

        [TestMethod]
        public void Test_VerifyGuestAllowedChars()
        {
            VerificationService verificationService = new VerificationService(_locationRepository, _eventRepository, _budgetRepository);
            Guest guest = new Guest() { FirstName = "John", LastName = "123"};
            Guest guest2 = new Guest() { FirstName = "John", LastName = "Doe" };
            bool result = verificationService.VerifyGuestName(guest);
            bool result2 = verificationService.VerifyGuestName(guest2);
            Assert.IsFalse(result);
            Assert.IsTrue(result2);
        }

        [TestMethod]
        public void Test_VerifyEvent()
        {
            VerificationService verificationService = new VerificationService(_locationRepository, _eventRepository, _budgetRepository);
            Event newEvent = new Event
                { Id = 99999, Type = "", Size = "Small", Name = "birthdayEvent", Status = "" };
            Event newEvent2 = new Event
                { Id = 99999, Type = "Birthday", Size = "Small", Name = "event123", Status = "" };
            Event newEvent3 = new Event
                { Id = 99999, Type = "Birthday", Size = "Small", Name = "birthdayEvent", Status = "" };
            bool validEvent = verificationService.VerifyEvent(newEvent);
            bool validEvent2 = verificationService.VerifyEvent(newEvent2);
            bool validEvent3 = verificationService.VerifyEvent(newEvent3);
            Assert.IsFalse(validEvent);
            Assert.IsFalse(validEvent2);
            Assert.IsTrue(validEvent3);
        }

        [TestMethod]
        public void Test_BudgetPrices()
        {
            VerificationService verificationService = new VerificationService(_locationRepository, _eventRepository, _budgetRepository);
            bool validPrices = verificationService.VerifyBudgetPrice("123");
            bool validPrices2 = verificationService.VerifyBudgetPrice("!#$%");
            Assert.IsTrue(validPrices);
            Assert.IsFalse(validPrices2);
        }

        [TestMethod]
        public void Test_FormatTime()
        {
            DateAndTimeService dateAndTimeService = new DateAndTimeService();
            Dictionary<string, string> dictWithTime = new Dictionary<string, string>()
            {
                { "StartHour", "1" }, {"StartMinutes", "20"}, {"TimeOfDayStart", "PM"},
                { "EndHour", "2" }, {"EndMinutes", "30"}, {"TimeOfDayEnd", "PM"}
            };
            Dictionary<string, string> dictWithTime2 = new Dictionary<string, string>()
            {
                { "StartHour", "11" }, {"StartMinutes", "20"}, {"TimeOfDayStart", "AM"},
                { "EndHour", "5" }, {"EndMinutes", "00"}, {"TimeOfDayEnd", "PM"}
            };
            Dictionary<string, string> result = dateAndTimeService.FormatDateInfo(dictWithTime);
            Dictionary<string, string> result2 = dateAndTimeService.FormatDateInfo(dictWithTime2);
            Assert.AreEqual("1:20 PM", result["StartTime"]);
            Assert.AreEqual("2:30 PM", result["EndTime"]);
            Assert.AreEqual("11:20 AM", result2["StartTime"]);
            Assert.AreEqual("5:00 PM", result2["EndTime"]);

        }
            
        
    }
}