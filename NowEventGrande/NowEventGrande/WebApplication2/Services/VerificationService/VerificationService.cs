using System.Net.Mail;
using WebApplication2.Data;
using WebApplication2.Models;

namespace WebApplication2.Services.VerificationService
{
    public class VerificationService : IVerificationService
    {
        private readonly ILocationRepository _locationRepository;
        private readonly IEventRepository _eventRepository;
        private Dictionary<string, string> _verificationInfo = new Dictionary<string, string>();
        private Dictionary<string, string> _openingHours = new Dictionary<string, string>();

        public VerificationService(ILocationRepository locationRepository, IEventRepository eventRepository)
        {
            _locationRepository = locationRepository;
            _eventRepository = eventRepository;
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
                _openingHours[day] = dayHours;
            }

            var eventStartHours = _eventRepository.GetEventStartTime(id).ToShortTimeString().Replace(":", ",");
            var startHour = decimal.Parse(eventStartHours);
            var eventEndHours = _eventRepository.GetEventEndTime(id).ToShortTimeString().Replace(":", ",");
            var endHour = decimal.Parse(eventEndHours);

            var dayOfWeek = _eventRepository.GetEventStartDate(id).DayOfWeek;
            System.Globalization.CultureInfo pl = new System.Globalization.CultureInfo("pl-PL");
            string dayOfWeekPl = pl.DateTimeFormat.DayNames[(int)dayOfWeek];

            foreach (var day in _openingHours)
            {
                if (day.Key == dayOfWeekPl.ToLower())
                {
                    var formatedHours = day.Value.Replace(":", ",");
                    var result = formatedHours.Split("–");
                    //TODO try to compare with TimeOfDay instead of parsing to decimals
                    // bool test = timeStart.TimeOfDay < choosedTime.TimeOfDay;
                    var openingHour = decimal.Parse(result[0]);
                    var closingHour = decimal.Parse(result[1]);

                    if (closingHour - openingHour < 0)
                    {
                        if (openingHour > startHour && startHour > closingHour)
                        {
                            _verificationInfo["EventStartStatus"] =
                                $"The start time of the event does not match the operating hours of the selected venue. On {dayOfWeek} this venue is open since {result[0].Replace(",", ":")}.";
                        }
                        else
                        {
                            _verificationInfo["EventStartStatus"] =
                                "The start time of the event match the operating hours of the selected venue. Yay!";
                        }

                        if (openingHour > endHour && endHour > closingHour)
                        {
                            _verificationInfo["EventEndStatus"] =
                                $"The end time of the event does not match the operating hours of the selected venue. On {dayOfWeek} this venue is closing at {result[1].Replace(",", ":")}.";
                        }
                        else
                        {
                            _verificationInfo["EventEndStatus"] =
                                "The end time of the event match the operating hours of the selected venue. Nice!";
                        }
                    }

                    if (closingHour - openingHour > 0)
                    {
                        if (openingHour < startHour || startHour > closingHour)
                        {
                            _verificationInfo["EventStartStatus"] =
                                $"The start time of the event does not match the operating hours of the selected venue. On {dayOfWeek} this venue is open since {result[0].Replace(",", ":")}.";
                        }
                        else
                        {
                            _verificationInfo["EventStartStatus"] =
                                "The start time of the event match the operating hours of the selected venue. Yay!";
                        }

                        if (openingHour < endHour || endHour > closingHour)
                        {
                            _verificationInfo["EventEndStatus"] =
                                $"The end time of the event does not match the operating hours of the selected venue. On {dayOfWeek} this venue is closing at {result[1].Replace(",", ":")}.";
                        }
                        else
                        {
                            _verificationInfo["EventEndStatus"] =
                                "The end time of the event match the operating hours of the selected venue. Nice!";
                        }
                    }
                }
            }
        }

        public bool VerifyGuest(Guest guest)
        {
            bool validFirstName = guest.FirstName.All(Char.IsLetter);
            bool validLastName = guest.LastName.All(Char.IsLetter);
            var validMail = true;

            try
            {
                var emailAddress = new MailAddress(guest.Email);
            }
            catch
            {
                validMail = false;
            }
            if (validFirstName && validLastName && validMail)
            {
                return true;
            }
            else
                return false;
        }

        public bool VerifyEvent(Event newEvent)
        {
            if (newEvent.Type == "")
            {
                return false;
            }
            return newEvent.Name.All(Char.IsLetter);
        }

    }
}
