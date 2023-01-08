using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using NowEvent.Models;

namespace NowEvent.Services.AuthenticationService
{
    public interface IUserAuthenticationService
    {
        Task<string> CreateTokenAsync(User user);
        SigningCredentials GetSigningCredentials();
        Task<List<Claim>> GetClaims(User user);
        JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims);
        string GetCurrentUserId(string userName);
    }
}
