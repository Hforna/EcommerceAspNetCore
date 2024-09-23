using EcommerceAspNet.Domain.Repository.Security;

namespace EcommerceAspNet.Api.Filters
{
    public class GetUserLoggedToken : IHttpContextAccessor, IGetUserLoggedToken 
    {
        public HttpContext? HttpContext { get; set; }

        public GetUserLoggedToken(HttpContext? httpContext) => HttpContext = httpContext;

        public string GetToken()
        {
            var token = HttpContext!.Request.Headers.Authorization.ToString();

            return token["Bearer ".Length..].Trim();
        }
    }
}
