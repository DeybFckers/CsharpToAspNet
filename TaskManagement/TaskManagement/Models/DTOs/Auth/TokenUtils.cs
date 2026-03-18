using Microsoft.IdentityModel.Tokens;
using System.Buffers.Text;
using System.Security.Cryptography;

namespace TaskManagement.Models.DTOs.Auth
{
    public class TokenUtils
    {
        public static (string rawToken, string hashedToken) GenerateRefreshToken()
        {
            var bytes = RandomNumberGenerator.GetBytes(64);
            var raw = Base64UrlEncoder.Encode(bytes);

            using var sha = SHA256.Create();
            var hashed = Convert.ToBase64String(sha.ComputeHash(bytes));
            return (raw, hashed);
        }

        public static string HashToken(string token)
        {
            var bytes = Convert.FromBase64String(token);
            using var sha = SHA256.Create();
            return Convert.ToBase64String(sha.ComputeHash(bytes));
        }
    }
}
