using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Infrastructure.Security.Token
{
    public class JwtTokenSecurityKey
    {
        public SecurityKey AsSecurityKey(string signKey)
        {
            var bytes = Encoding.UTF8.GetBytes(signKey);

            return new SymmetricSecurityKey(bytes);
        }
    }
}
