using System.Net.Mail;
using NowEvent.Data;
using NowEvent.Data.Repositories.BudgetRepository;
using NowEvent.Data.Repositories.EventRepository;
using NowEvent.Data.Repositories.LocationAndTimeRepository;
using NowEvent.Models;
using NowEvent.Models.Constants;
using NowEvent.Services.BudgetService;
using NowEvent.Services.DateAndTimeService;

namespace NowEvent.Services.VerificationService
{
    public class VerificationService : IVerificationService
    {
        private readonly ILocationAndTimeRepository _locationRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IBudgetService _budgetService;
        private readonly IDateAndTimeService _dateAndTimeService;
        private readonly Dictionary<string, string> _verificationInfo = new()
        {
            {PlaceStatuses.Title, PlaceStatuses.NoDataMessage}
        };
        private Dictionary<string, string> _allOpeningHours = new ();
        private DateTime _openingHour;
        private DateTime _closingHour;
        private string _openingTimeOnly = "--";
        private string _closingTimeOnly = "--";


        public VerificationService(ILocationAndTimeRepository locationRepository, IEventRepository eventRepository, 
            IBudgetService budgetService, IDateAndTimeService dateAndTimeService)
        {
            _locationRepository = locationRepository;
            _eventRepository = eventRepository;
            _budgetService = budgetService;
            _dateAndTimeService = dateAndTimeService;
        }

        public Dictionary<string, string> GetVerificationInfo(int eventId)
        {
            var location = _locationRepository.GetLocation(eventId);
            if (VerifyIfRecordExists(location))
            {
                VerifyPlaceStatus(location.PlaceStatus);
                VerifyPlaceHours(location.PlaceOpeningHours, eventId);
            }
            return _verificationInfo;

        }

        public bool VerifyIfRecordExists<T>(T record)
        {
            return record != null;
        }

        private void VerifyPlaceStatus(string placeStatus)
        {
            switch (placeStatus)
            {
                case PlaceStatuses.OperationalStatus:
                    _verificationInfo[PlaceStatuses.Title] = PlaceStatuses.OperationalMessage;
                    break;
                case PlaceStatuses.ClosedTempStatus:
                    _verificationInfo[PlaceStatuses.Title] = PlaceStatuses.ClosedTempMessage;
                    break;
                case PlaceStatuses.ClosedPermStatus:
                    _verificationInfo[PlaceStatuses.Title] = PlaceStatuses.ClosedPermMessage;
                    break;
            }
        }

        private void VerifyPlaceHours(string allDaysAndHours, int id)
        {
            _allOpeningHours = _dateAndTimeService.FormatAllOpeningDaysAndHours(allDaysAndHours);
            var startDate = _eventRepository.GetEventTimeStage(id, EventTimeStages.Start);
            var endDate = _eventRepository.GetEventTimeStage(id, EventTimeStages.End);
            var dayOfWeek =  _eventRepository.GetEventStartDate(id);
            System.Globalization.CultureInfo pl = new System.Globalization.CultureInfo("pl-PL");
            string dayOfWeekPl = pl.DateTimeFormat.DayNames[(int)dayOfWeek.DayOfWeek];

            foreach (var day in _allOpeningHours)
            {
                if (day.Key != dayOfWeekPl.ToLower()) continue;

                _openingHour = _dateAndTimeService.GetOperationalHour(day.Value, EventTimeStages.Start, startDate);
                _closingHour = _dateAndTimeService.GetOperationalHour(day.Value, EventTimeStages.End, endDate);
                bool isStartTimeCorrect = CompareOpeningAndClosingHours(startDate);
                bool isEndTimeCorrect = CompareOpeningAndClosingHours(endDate);
                SetEventTimeStatus(dayOfWeek.DayOfWeek, isStartTimeCorrect, EventTimeStages.Start);
                SetEventTimeStatus(dayOfWeek.DayOfWeek, isEndTimeCorrect, EventTimeStages.End);
            }
        }

        public bool VerifyGuest(Guest guest)
        {
            bool validName = VerifyGuestName(guest);
            var validMail = true;
            try
            {
                new MailAddress(guest.Email);
            }
            catch
            {
                validMail = false;
            }
            return validName && validMail;
        }

        private bool VerifyGuestName(Guest guest)
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
            Dictionary<BudgetOptions, decimal> allPrices = _budgetService.GetAllPrices(eventId);
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
            bool isOpeningCorrect = openingCompareResult >= 0;
            bool isClosingCorrect = closingCompareResult >= 0;
            return isOpeningCorrect && isClosingCorrect;
        }

        public void SetEventTimeStatus(DayOfWeek dayOfWeek, bool isTimeCorrect, EventTimeStages timeStage )
        {
            _openingTimeOnly = _openingHour.ToString("hh:mm tt");
            _closingTimeOnly = _closingHour.ToString("hh:mm tt");

            switch (timeStage)
            {
                case EventTimeStages.Start:
                    if (!isTimeCorrect)
                    {
                        _verificationInfo[EventStatuses.EventStartStatusTitle] =
                            CreateVerificationNote(EventTimeStages.Start, dayOfWeek);
                    }
                    else
                        _verificationInfo[EventStatuses.EventStartStatusTitle] =
                            CreateVerificationNote(EventTimeStages.Start);
                    break;

                case EventTimeStages.End:
                    if (!isTimeCorrect)
                    {
                        _verificationInfo[EventStatuses.EventEndStatusTitle] =
                            CreateVerificationNote(EventTimeStages.End, dayOfWeek);
                    }
                    else
                        _verificationInfo[EventStatuses.EventEndStatusTitle] =
                            CreateVerificationNote(EventTimeStages.End);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(timeStage), timeStage, "invalid status");
            }
        }

        public bool VerifyTheme(string theme)
        {
            bool exists = Enum.IsDefined(typeof(Themes), theme);
            return exists;
        }

        public string CreateVerificationNote(EventTimeStages timeStage)
        {
            return $"The {timeStage.ToString().ToLower()} time of the event match " +
                   "the operating hours of the selected venue. Yay!";
        }
        public string CreateVerificationNote(EventTimeStages timeStage, DayOfWeek dayOfWeek)
        {
            return $"The {timeStage.ToString().ToLower()} time of the event does not match " +
                   "the operating hours of the selected venue. " +
                   $"On {dayOfWeek} this venue is open since {_openingTimeOnly} " +
                   $"and closing at {_closingTimeOnly}.";
        }

        public bool VerifyEventDateAndTime(Dictionary<string, string> formattedDateInfo)
        {
            bool isDateCorrect = DateTime.TryParse(formattedDateInfo[EventInfoFields.Date], out _);
            bool correctStartTime = DateTime.TryParse(formattedDateInfo[Date.StartTime], out _);
            bool correctEndTime = DateTime.TryParse(formattedDateInfo[Date.EndTime], out _);

            return isDateCorrect && correctStartTime && correctEndTime;
        }
    }
}
