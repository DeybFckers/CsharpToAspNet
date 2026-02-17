using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RolebaseAuthentication.Models;
using RolebaseAuthentication.Repositories.Interface;
using RolebaseAuthentication.Services.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RolebaseAuthentication.Services.Implementation
{
    public class AuthServices : IAuthServices
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtOptions _jwtOptions;

        public AuthServices(IUserRepository userRepository, IOptions<JwtOptions> jwt)
        {
            _userRepository = userRepository;
            _jwtOptions = jwt.Value;
        }

        public async Task<string?> LoginAsync(Login model)
        {
            var user = await _userRepository.GetUserByUsername(model.Username);
            if (user == null) return null;

            if(!await _userRepository.CheckPassword(user, model.Password)) return null;

            var roles = await _userRepository.GetUserRoles(user);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtOptions.ExpiresInMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<bool> RegisterAsync(Register model)
        {
            var result = await _userRepository.CreateUser(model);
            if (!result.Succeeded) return false;

            var user = await _userRepository.GetUserByUsername(model.Username);
            if(user != null)
            {
                await _userRepository.AddUserToRole(user, "User");
                return true;
            }

            return false;
        }
    }
}
