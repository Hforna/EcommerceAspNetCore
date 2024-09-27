using EcommerceAspNet.Domain.Repository.Security;

namespace EcommerceAspNet.Api.Filters
{
    public class GetUserLoggedToken : IGetUserLoggedToken 
    {
        public IHttpContextAccessor? HttpAccessor { get; set; }

        public GetUserLoggedToken(IHttpContextAccessor httpContext) => HttpAccessor = httpContext;

        public string GetToken()
        {
            var token = HttpAccessor!.HttpContext!.Request.Headers.Authorization.ToString();

            return token["Bearer ".Length..].Trim();
        }
    }
}
