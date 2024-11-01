using EcommerceAspNet.Domain.Entitie.User;
using EcommerceAspNet.Domain.Repository.Security;
using EcommerceAspNet.Infrastructure.DataEntity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Infrastructure.Security.Token
{
    public class GetUserByToken : IGetUserByToken
    {
        private readonly IGetUserLoggedToken _userLoggedToken;
        private readonly ProjectDbContext _dbContext;

        public GetUserByToken(IGetUserLoggedToken userLoggedToken, ProjectDbContext dbContext)
        {
            _userLoggedToken = userLoggedToken;
            _dbContext = dbContext;
        }

        public async Task<User?> GetUser()
        {
            var token = _userLoggedToken.GetToken();

            if (string.IsNullOrEmpty(token))
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var readToken = tokenHandler.ReadJwtToken(token);
            var guidToken = Guid.Parse(readToken.Claims.First(d => d.Type == ClaimTypes.Sid).Value);

            return await _dbContext.Users.Include(d => d.UserOrder).FirstOrDefaultAsync(u => u.UserIdentifier == guidToken && u.Active);
        }
    }
}
