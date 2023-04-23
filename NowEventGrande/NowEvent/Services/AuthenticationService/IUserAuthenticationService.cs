using NowEvent.Models;

namespace NowEvent.Services.AuthenticationService
{
    public interface IUserAuthenticationService
    {
        Task<string> CreateTokenAsync(User user);
        string GetCurrentUserId(string userName);
    }
}
