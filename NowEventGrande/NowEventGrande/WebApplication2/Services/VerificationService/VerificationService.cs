using System.Net.Mail;
using WebApplication2.Data;
using WebApplication2.Models;

namespace WebApplication2.Services.VerificationService
{
    public class VerificationService : IVerificationService
    {
        private readonly ILocationAndTimeRepository _locationRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IBudgetRepository _budgetRepository;
        private Dictionary<string, string> _verificationInfo = new Dictionary<string, string>();
        private Dictionary<string, string> _allOpeningHours = new Dictionary<string, string>();
        private DateTime _openingHour;
        private DateTime _closingHour;

        public VerificationService(ILocationAndTimeRepository locationRepository, IEventRepository eventRepository, 
            IBudgetRepository budgetRepository)
        {
            _locationRepository = locationRepository;
            _eventRepository = eventRepository;
            _budgetRepository = budgetRepository;
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

        public void VerifyPlaceHours(string allHours, int id)
        {
            var hours = allHours.Split('\u002C');
            foreach (var hour in hours)
            {
                var day = hour.Substring(0, hour.Length - 13);
                var dayHours = hour.Substring(hour.Length - 11);
                _allOpeningHours[day] = dayHours;
            }

            var startHour = _eventRepository.GetEventStartTime(id);
            var endHour = _eventRepository.GetEventEndTime(id);
            DayOfWeek dayOfWeek = _eventRepository.GetEventStartDate(id).DayOfWeek;
            System.Globalization.CultureInfo pl = new System.Globalization.CultureInfo("pl-PL");
            string dayOfWeekPl = pl.DateTimeFormat.DayNames[(int)dayOfWeek];

            foreach (var day in _allOpeningHours)
            {
                if (day.Key != dayOfWeekPl.ToLower()) continue;

                var openingAndClosingHours = day.Value.Split("–");
                var openingHoursAndMinutes = openingAndClosingHours[0].Split(":");
                var closingHoursAndMinutes = openingAndClosingHours[1].Split(":");

                //TODO if hour over midnight add one day
                _openingHour = new DateTime(startHour.Year, startHour.Month, startHour.Day,
                    int.Parse(openingHoursAndMinutes[0]), int.Parse(openingHoursAndMinutes[1]), 00);
                _closingHour = new DateTime(endHour.Year, endHour.Month, endHour.Day,
                    int.Parse(closingHoursAndMinutes[0]), int.Parse(closingHoursAndMinutes[1]), 00);

                bool isStartTimeCorrect = CompareOpeningAndClosingHours(startHour);
                bool isEndTimeCorrect = CompareOpeningAndClosingHours(endHour);
                SetEventTimeStatus(openingAndClosingHours, dayOfWeek, isStartTimeCorrect, EventTimeStages.Start);
                SetEventTimeStatus(openingAndClosingHours, dayOfWeek, isEndTimeCorrect, EventTimeStages.End);
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

        public void SetEventTimeStatus(string[] result, DayOfWeek dayOfWeek, bool isTimeCorrect, EventTimeStages timeStage )
        {
            switch (timeStage)
            {
                case EventTimeStages.Start:
                    if (!isTimeCorrect)
                    {
                        _verificationInfo["EventStartStatus"] =
                            $"The start time of the event does not match the operating hours of the selected venue. On {dayOfWeek} this venue is open since {result[0]} and closing at {result[1]}.";
                    }
                    else
                        _verificationInfo["EventStartStatus"] =
                            "The start time of the event match the operating hours of the selected venue. Yay!";
                    break;

                case EventTimeStages.End:
                    if (!isTimeCorrect)
                    {
                        _verificationInfo["EventEndStatus"] =
                            $"The end time of the event does not match the operating hours of the selected venue. On {dayOfWeek} this venue is open since {result[0]} and closing at {result[1]}.";
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
