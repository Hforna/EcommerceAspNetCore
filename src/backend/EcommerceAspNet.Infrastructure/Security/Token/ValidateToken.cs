using EcommerceAspNet.Domain.Repository.Security;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Infrastructure.Security.Token
{
    public class ValidateToken : JwtTokenSecurityKey, IValidateToken
    {
        private readonly string _signKey;

        public ValidateToken(string signKey) => _signKey = signKey;

        public Guid Validate(string token)
        {
            var parameters = new TokenValidationParameters()
            {
                ClockSkew = new TimeSpan(0),
                ValidateAudience = false,
                ValidateIssuer = false,
                IssuerSigningKey = AsSecurityKey(_signKey)
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var validateToken = tokenHandler.ValidateToken(token, parameters, out _);

            var tokenGuid = validateToken.Claims.First(c => c.Type == ClaimTypes.Sid).Value;
            var toGuid = Guid.Parse(tokenGuid);
            
            return toGuid;
        }
    }
}
