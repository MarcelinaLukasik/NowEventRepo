using NowEvent.Models;

namespace NowEvent.Services.DateAndTimeService
{
    public interface IDateAndTimeService
    {
        Dictionary<string, string> FormatDateInfo(Dictionary<string, string> dateInfo);
        Dictionary<string, string> FormatAllOpeningDaysAndHours(string allDaysAndHours);
        DateTime GetOperationalHour(string dayInfo, EventTimeStages timeStage, DateTime date);
        void UpdateStatuses();
    }
}
