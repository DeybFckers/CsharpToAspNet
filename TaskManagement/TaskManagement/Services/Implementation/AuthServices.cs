using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManagement.Models;
using TaskManagement.Models.DTOs.Auth;
using TaskManagement.Models.Entities;
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
            
            //Generate Refresh Token
            var (rawToken, hashedToken) = TokenUtils.GenerateRefreshToken();

            var refreshToken = new RefreshToken
            {
                Id = Guid.NewGuid(),
                Token = hashedToken,
                UserId = user.Id,
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                IsRevoked = false
            };

            await _authRepository.SaveRefreshToken(refreshToken);

            return new AuthResponseDto
            {
                AccessToken = tokenString,
                RefreshToken = rawToken,
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

        public async Task<AuthResponseDto?> RefreshToken(string refreshToken)
        {
            var hashedToken = TokenUtils.HashToken(refreshToken);

            var storedToken = await _authRepository.GetRefreshToken(hashedToken);

            if (storedToken == null || storedToken.ExpiresAt < DateTime.UtcNow)
                return null;

            var user = storedToken.User;

            // revoke old token (rotation)
            await _authRepository.RevokeRefreshToken(storedToken);

            // generate new access + refresh token
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

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expires = DateTime.UtcNow.AddMinutes(_jwtOptions.ExpiresInMinutes);

            var newJwt = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            var newAccessToken = new JwtSecurityTokenHandler().WriteToken(newJwt);

            var (newRaw, newHashed) = TokenUtils.GenerateRefreshToken();

            var newRefreshToken = new RefreshToken
            {
                Id = Guid.NewGuid(),
                Token = newHashed,
                UserId = user.Id,
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                IsRevoked = false
            };

            await _authRepository.SaveRefreshToken(newRefreshToken);

            return new AuthResponseDto
            {
                AccessToken = newAccessToken,
                RefreshToken = newRaw,
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
