using HotelBookingApi.Contracts.IServices;
using HotelBookingApi.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HotelBookingApi.Services
{
    public class TokenService : ITokenServices
    {
        private readonly JwtOptions _jwt;

        public TokenService(IOptions<JwtOptions> options)
        {
            _jwt = options.Value;
        }

        public string GenerateToken(UsersTokenDto users)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, users.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, users.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwt.ExpiresInMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
