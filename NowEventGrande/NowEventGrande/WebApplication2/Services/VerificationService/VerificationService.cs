using WebApplication2.Data;

namespace WebApplication2.Services.VerificationService
{
    public class VerificationService : IVerificationService
    {
        private readonly ILocationRepository _locationRepository;
        private Dictionary<string, string> _verificationInfo = new Dictionary<string, string>();

        public VerificationService(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public Dictionary<string, string> GetVerificationInfo(int eventId)
        {
            var location = _locationRepository.GetLocationAddress(eventId);
            VerifyPlaceStatus(location.PlaceStatus);

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
    }
}
