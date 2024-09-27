using EcommerceAspNet.Api.Filters;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAspNet.Api.Attibutes
{
    public class AuthenticationUserAttribute : TypeFilterAttribute
    {
        public AuthenticationUserAttribute() : base(typeof(TokenFilter))
        {
        }
    }
}
