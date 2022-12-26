namespace WebApplication2.Services.DateAndTimeService
{
    public class DateAndTimeService : IDateAndTimeService
    {
        public Dictionary<string, string> FormatDateInfo(Dictionary<string, string> dateInfo)
        {
            dateInfo["StartTime"] = dateInfo["StartHour"] + ":" + dateInfo["StartMinutes"] + " " + dateInfo["TimeOfDayStart"];
            dateInfo["EndTime"] = dateInfo["EndHour"] + ":" + dateInfo["EndMinutes"] + " " + dateInfo["TimeOfDayEnd"];

            return dateInfo;
        }
    }
}
