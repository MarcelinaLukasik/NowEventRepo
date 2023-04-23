using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using NowEvent.Models;

namespace NowEvent.Services.AuthenticationService
{
    public interface IUserAuthenticationService
    {
        Task<string> CreateTokenAsync(User user);
        string GetCurrentUserId(string userName);
    }
}
