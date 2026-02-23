using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManagement.Models;
using TaskManagement.Models.DTOs.Auth;
using TaskManagement.Repositories.Interface;
using TaskManagement.Services.Interface;

namespace TaskManagement.Services.Implementation
{
    public class AuthServices : IAuthServices
    {
        private readonly IAuthRepository _authRepository;
        private readonly JwtOptions _jwtOptions;

        public AuthServices(IAuthRepository authRepository, IOptions<JwtOptions>jwt)
        {
            _authRepository = authRepository;
            _jwtOptions = jwt.Value;
        }

        public async Task<AuthResponseDto> Login(Login login)
        {
            var user = await _authRepository.GetUserByEmail(login.Email);
            if (user == null) return null;

            if (!await _authRepository.CheckPassword(user, login.Password))
                return null;

            var roles = await _authRepository.GetUserRoles(user);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email!)
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwtOptions.Key)
            );

            var creds = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256
            );

            var expires = DateTime.UtcNow.AddMinutes(_jwtOptions.ExpiresInMinutes);

            var token = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return new AuthResponseDto
            {
                AccessToken = tokenString,
                ExpiresIn = _jwtOptions.ExpiresInMinutes * 60,
                User = new UserDto
                {
                    Id = user.Id,
                    Email = user.Email!,
                    fullname = new UserFullnameDto
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName
                    },
                    Roles = roles.ToArray()
                }
            };

        }

        public async Task<bool> Register(Register register)
        {
            var result = await _authRepository.CreateUser(register);

            if (!result.Succeeded) return false;

            var user = await _authRepository.GetUserByEmail(register.Email);

            if (user != null)
            {
                await _authRepository.AddRolesToUser(user, new[] { "User" });
                return true;
            }
            return false;

        }
    }
}
