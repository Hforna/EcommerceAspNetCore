using EcommerceAspNet.Domain.Repository.Security;
using EcommerceAspNet.Exception.Exception;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EcommerceAspNet.Api.Filters
{
    public class TokenFilter : IAsyncAuthorizationFilter
    {
        private readonly IValidateToken _validateToken;
        private readonly IGetUserLoggedToken _userToken;
        private readonly IGetUserByToken _userByToken;

        public TokenFilter(IValidateToken validateToken, IGetUserByToken userByToken, IGetUserLoggedToken getUserLoggedToken)
        {
            _validateToken = validateToken;
            _userByToken = userByToken;
            _userToken = getUserLoggedToken;
        }
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var token = _userToken.GetToken();

            _validateToken.Validate(token);

            var user = await _userByToken.GetUser();

            if(user is null)
            {
                throw new UserException("User doesn't exists");
            }
        }
    }
}
