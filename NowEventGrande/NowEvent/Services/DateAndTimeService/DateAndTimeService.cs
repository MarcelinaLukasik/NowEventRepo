using NowEvent.Data;
using NowEvent.Models;

namespace NowEvent.Services.DateAndTimeService
{
    public class DateAndTimeService : IDateAndTimeService
    {
        private Dictionary<string, string> _allOpeningHours = new Dictionary<string, string>();
        private readonly IEventRepository _eventRepository;

        public DateAndTimeService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }
        public Dictionary<string, string> FormatDateInfo(Dictionary<string, string> dateInfo)
        {
            dateInfo["StartTime"] = dateInfo["StartHour"] + ":" + dateInfo["StartMinutes"] + " " + dateInfo["TimeOfDayStart"];
            dateInfo["EndTime"] = dateInfo["EndHour"] + ":" + dateInfo["EndMinutes"] + " " + dateInfo["TimeOfDayEnd"];

            return dateInfo;
        }

        public Dictionary<string, string> FormatAllOpeningDaysAndHours(string allDaysAndHours)
        {
            if (allDaysAndHours != "undefined")
            {
                var daysAndHours = allDaysAndHours.Split('\u002C');
                foreach (var dayAndHour in daysAndHours)
                {
                    var separatedDayAndHour = dayAndHour.Split(" ");
                    var day = separatedDayAndHour[0].Replace(":", "");
                    _allOpeningHours[day] = separatedDayAndHour[1];
                }
            }
            return _allOpeningHours;
        }

        public DateTime GetOperationalHour(string dayInfo, EventTimeStages timeStage, DateTime date)
        {
            var openingAndClosingHours = dayInfo.Split("–");
            switch (timeStage)
            {
                case EventTimeStages.Start:
                    var openingHoursAndMinutes = openingAndClosingHours[0].Split(":");
                    return new DateTime(date.Year, date.Month, date.Day,
                        int.Parse(openingHoursAndMinutes[0]), int.Parse(openingHoursAndMinutes[1]), 00);
                case EventTimeStages.End:
                    var closingHoursAndMinutes = openingAndClosingHours[1].Split(":");
                    return new DateTime(date.Year, date.Month, date.Day,
                        int.Parse(closingHoursAndMinutes[0]), int.Parse(closingHoursAndMinutes[1]), 00);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        public async void UpdateStatuses()
        {
            var offers = _eventRepository.GetAllOffers();
            foreach (Event evt in offers.Result)
            {
                if (evt.Date <= DateTime.Now)
                {
                    await _eventRepository.SetStatus(evt.Id, EventStatuses.Finished);
                }

            }
        }
    }
}
