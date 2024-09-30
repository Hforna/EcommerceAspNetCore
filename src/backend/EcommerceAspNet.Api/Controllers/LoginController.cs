using EcommerceAspNet.Application.UseCase.Repository.Login;
using EcommerceAspNet.Communication.Request.User;
using EcommerceAspNet.Communication.Response.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EcommerceAspNet.Api.Controllers
{
    public class LoginController : BaseController
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseCreateUser), StatusCodes.Status200OK)]
        public async Task<IActionResult> Login([FromBody] RequestLoginUser request, [FromServices] ILoginUseCase useCase)
        {
            var result = await useCase.Execute(request);

            return Ok(result);
        }

        [HttpGet]
        [Route("google")]
        public async Task<IActionResult> LoginGoogle([FromServices] ILoginGoogleUseCase useCase, string returnUrl)
        {
            var result = await Request.HttpContext.AuthenticateAsync("Google");

            if(IsNotAuthenticated(result))
            {
                return Challenge(GoogleDefaults.AuthenticationScheme);
            } else
            {
                var claims = result.Principal!.Identities.First().Claims;

                var name = claims.First(d => d.Type == ClaimTypes.Name).Value;
                var email = claims.First(d => d.Type == ClaimTypes.Email).Value;

                await useCase.Execute(name, email);

                return Redirect(returnUrl);
            }
        }

        private static bool IsNotAuthenticated(AuthenticateResult result)
        {
            return result.Succeeded == false
                || result.Principal is null
                || result.Principal.Identities.Any(d => d.IsAuthenticated) == false;
        }
    }
}
