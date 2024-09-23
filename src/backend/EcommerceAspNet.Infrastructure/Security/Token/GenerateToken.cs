using EcommerceAspNet.Domain.Repository.Security;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Infrastructure.Security.Token
{
    public class GenerateToken : IGenerateToken
    {
        private readonly long _minutesExpire;
        private readonly string _signKey;

        public GenerateToken(string signKey, long minutesExpire)
        {
            _signKey = signKey;
            _minutesExpire = minutesExpire;
        }
            
        public string Generate(Guid uid)
        {
            List<Claim> claims = new List<Claim>() { new Claim(ClaimTypes.Sid, uid.ToString()) };
            var descriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_minutesExpire),
                SigningCredentials = new SigningCredentials(GetSignKey(), SecurityAlgorithms.HmacSha256)
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var createToken = tokenHandler.CreateToken(descriptor);

            return tokenHandler.WriteToken(createToken);
        }

        public SecurityKey GetSignKey()
        {
            var bytes = Encoding.UTF8.GetBytes(_signKey);

            return new SymmetricSecurityKey(bytes);
        }
    }
}
