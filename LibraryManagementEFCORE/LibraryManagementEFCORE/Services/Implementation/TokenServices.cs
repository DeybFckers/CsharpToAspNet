using LibraryManagementEFCORE.Models;
using LibraryManagementEFCORE.Models.DTOs;
using LibraryManagementEFCORE.Services.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LibraryManagementEFCORE.Services.Implementation
{
    public class TokenServices : ITokenServices
    {
        private readonly JwtOptions _jwt;

        public TokenServices(IOptions<JwtOptions> jwt)
        {
            _jwt = jwt.Value;
        }

        public string GenerateToken(int id, string email)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
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
