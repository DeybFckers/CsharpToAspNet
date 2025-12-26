using FirstWebApi.Contracts;
using FirstWebApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FirstWebApi.Services
{
    //Implementation of Token Service

    //inherit then implement interface
    public class TokenServices : ITokenService
    {
        private readonly TokenValidationParameters _validationParameters;
        private readonly string _secret;
        public TokenServices(TokenValidationParameters validationParameters, string secret)
        {
            //in constuctor we have one paramaters which is TokenValidationParameters
            //and everytime that we get a value we will just pass to
            //variable ( _validationParameters )
            _validationParameters = validationParameters;
            _secret = secret;
        }

        public string GenerateToken(User user)
        {
            // we will use the parameters to create the block of code which is userClaims
            // we will use this code for our payload of data and it means if someone
            // pass a valid token, we can extract their payload and we can get the user id and email
            var userClaims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                //in first item we use an id for nameindentifier
                new Claim(ClaimTypes.Name, user.Email)
                //in second item is for name and the value we will use is Email
            };

            //key or signature of out token and the value should be same on creation of validation(program.cs)
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret));

            // we create a credentials using the key variable and
            // we use securityAlgorithms.hmacsha256signature to encrypt our token
            // and it requires to create scecret key with 16 Characters and
            // put it on appsettings so validation and generation are same
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            
            //this is the way to create a instance for our token
                                     //we passed the userClaims  //we passed the credentials
            var token = new JwtSecurityToken(
                claims: userClaims, 
                signingCredentials: credentials, 
                expires: DateTime.Now.AddMinutes(3)
                );

            return new JwtSecurityTokenHandler()
                .WriteToken(token); 
        }

        //we can use this isvalid if we will use create a endpoint of tokenvalidator or custom authorization
        public bool IsValid(string token)
        {
            try
            {
                //instaniate
                var tokenHandler = new JwtSecurityTokenHandler();
                //call the tokaneHandler for validateToken
                //first parameter is from parameters token
                //second is from constructuor _validationParameters
                //lastly is out parameter of securitytoken it means we can get the value from securitytoken
                tokenHandler.ValidateToken(token, _validationParameters, out SecurityToken securityToken);

            }
            catch
            {
                return false;
            }

            return true;
        }
    }
    
}
