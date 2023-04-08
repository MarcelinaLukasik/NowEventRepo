namespace NowEvent.Models.Constants
{
    public static class PlaceStatuses
    {
        public const string Title = "PlaceStatus";
        public const string NoDataMessage = "No information. Provide location and come back.";
        public const string NoAddress = "No address set.";
        public const string OperationalStatus = "OPERATIONAL";
        public const string ClosedTempStatus = "CLOSED_TEMPORARILY";
        public const string ClosedPermStatus = "CLOSED_PERMANENTLY";
        public const string OperationalMessage = "Looks like the place you chose is operational. Good!";
        public const string ClosedTempMessage = "Looks like the place you chose is temporarily closed. " +
                                                "Consider changing it to another one";
        public const string ClosedPermMessage = "Looks like the place you chose is permanently closed. " +
                                                "You need to chose a different one";
    }
}

