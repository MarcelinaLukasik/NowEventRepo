using System.Net.Mail;
using WebApplication2.Data;
using WebApplication2.Models;
using WebApplication2.Services.DateAndTimeService;

namespace WebApplication2.Services.VerificationService
{
    public class VerificationService : IVerificationService
    {
        private readonly ILocationAndTimeRepository _locationRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IBudgetRepository _budgetRepository;
        private readonly IDateAndTimeService _dateAndTimeService;
        private Dictionary<string, string> _verificationInfo = new Dictionary<string, string>();
        private Dictionary<string, string> _allOpeningHours = new Dictionary<string, string>();
        private DateTime _openingHour;
        private DateTime _closingHour;

        public VerificationService(ILocationAndTimeRepository locationRepository, IEventRepository eventRepository, 
            IBudgetRepository budgetRepository, IDateAndTimeService dateAndTimeService)
        {
            _locationRepository = locationRepository;
            _eventRepository = eventRepository;
            _budgetRepository = budgetRepository;
            _dateAndTimeService = dateAndTimeService;
        }

        public Dictionary<string, string> GetVerificationInfo(int eventId)
        {
            var location = _locationRepository.GetLocation(eventId);
            if (location != null)
            {
                VerifyPlaceStatus(location.PlaceStatus);
                VerifyPlaceHours(location.PlaceOpeningHours, eventId);
            }
            return _verificationInfo;

        }

        public void VerifyPlaceStatus(string placeStatus)
        {
            switch (placeStatus)
            {
                case "OPERATIONAL":
                    _verificationInfo["PlaceStatus"] = "Looks like the place you chose is operational. Good!";
                    break;
                case "CLOSED_TEMPORARILY":
                    _verificationInfo["PlaceStatus"] = "Looks like the place you chose is temporarily closed. Consider changing it to another one";
                    break;
                case "CLOSED_PERMANENTLY":
                    _verificationInfo["PlaceStatus"] = "Looks like the place you chose is permanently closed. You need to chose a different one";
                    break;
            }
        }

        public void VerifyPlaceHours(string allDaysAndHours, int id)
        {
            _allOpeningHours = _dateAndTimeService.FormatAllOpeningDaysAndHours(allDaysAndHours);
            var startDate = _eventRepository.GetEventTimeStage(id, EventTimeStages.Start);
            var endDate = _eventRepository.GetEventTimeStage(id, EventTimeStages.End);
            DayOfWeek dayOfWeek = _eventRepository.GetEventStartDate(id).DayOfWeek;
            System.Globalization.CultureInfo pl = new System.Globalization.CultureInfo("pl-PL");
            string dayOfWeekPl = pl.DateTimeFormat.DayNames[(int)dayOfWeek];

            foreach (var day in _allOpeningHours)
            {
                if (day.Key != dayOfWeekPl.ToLower()) continue;

                // var openingAndClosingHours = day.Value.Split("–");
                // var openingHoursAndMinutes = openingAndClosingHours[0].Split(":");
                // var closingHoursAndMinutes = openingAndClosingHours[1].Split(":");
                _openingHour = _dateAndTimeService.GetOperationalHour(day.Value, EventTimeStages.Start, startDate);
                _closingHour = _dateAndTimeService.GetOperationalHour(day.Value, EventTimeStages.End, endDate);

                bool isStartTimeCorrect = CompareOpeningAndClosingHours(startDate);
                bool isEndTimeCorrect = CompareOpeningAndClosingHours(endDate);
                SetEventTimeStatus(dayOfWeek, isStartTimeCorrect, EventTimeStages.Start);
                SetEventTimeStatus(dayOfWeek, isEndTimeCorrect, EventTimeStages.End);
            }
        }

        public bool VerifyGuest(Guest guest)
        {
            bool validName = VerifyGuestName(guest);
            var validMail = true;
            try
            {
                var emailAddress = new MailAddress(guest.Email);
            }
            catch
            {
                validMail = false;
            }
            return validName && validMail;
        }

        public bool VerifyGuestName(Guest guest)
        {
            bool validFirstName = guest.FirstName.All(Char.IsLetter);
            bool validLastName = guest.LastName.All(Char.IsLetter);
            return validFirstName && validLastName;
        }

        public bool VerifyEvent(Event newEvent)
        {
            return newEvent.Type != "" && newEvent.Name.All(Char.IsLetter);
        }

        public bool CheckBudgetFullStatus(int eventId)
        {
            Dictionary<BudgetPrices, decimal> allPrices = _budgetRepository.GetAllPrices(eventId);
            foreach (var price in allPrices)
            {
                if (price.Value <= 0)
                    return false;
            }
            return true;
        }

        public bool VerifyBudgetPrice(string budgetPrice)
        {
            return decimal.TryParse(budgetPrice, out _);
        }

        public bool CompareOpeningAndClosingHours(DateTime chosenHour)
        {
            int openingCompareResult = DateTime.Compare(chosenHour, _openingHour);
            int closingCompareResult = DateTime.Compare(_closingHour, chosenHour);
            return openingCompareResult >= 0 || closingCompareResult >= 0;
        }

        public void SetEventTimeStatus(DayOfWeek dayOfWeek, bool isTimeCorrect, EventTimeStages timeStage )
        {
            var openingTimeOnly = _openingHour.ToString("HH:mm");
            var closingTimeOnly = _closingHour.ToString("HH:mm");

            switch (timeStage)
            {
                case EventTimeStages.Start:
                    if (!isTimeCorrect)
                    {
                        
                        _verificationInfo["EventStartStatus"] =
                            $"The start time of the event does not match the operating hours of the selected venue. On {dayOfWeek} this venue is open since {openingTimeOnly} and closing at {closingTimeOnly}.";
                    }
                    else
                        _verificationInfo["EventStartStatus"] =
                            "The start time of the event match the operating hours of the selected venue. Yay!";
                    break;

                case EventTimeStages.End:
                    if (!isTimeCorrect)
                    {
                        _verificationInfo["EventEndStatus"] =
                            $"The end time of the event does not match the operating hours of the selected venue. On {dayOfWeek} this venue is open since {openingTimeOnly} and closing at {closingTimeOnly}.";
                    }
                    else
                        _verificationInfo["EventEndStatus"] =
                            "The end time of the event match the operating hours of the selected venue. Yay!";
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(timeStage), timeStage, null);
            }
        }

    }
}
