using EcommerceAspNet.Application.UseCase.Repository.Login;
using EcommerceAspNet.Communication.Request.User;
using EcommerceAspNet.Communication.Response.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    }
}
