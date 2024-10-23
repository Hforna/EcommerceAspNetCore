using EcommerceAspNet.Api.Attibutes;
using EcommerceAspNet.Application.UseCase.Repositorie.User;
using EcommerceAspNet.Application.UseCase.Repository.User;
using EcommerceAspNet.Communication.Request.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace EcommerceAspNet.Api.Controllers
{
    [EnableRateLimiting("tworequestlimiter")]
    public class UserController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RequestCreateUser request, [FromServices] ICreateUserUseCase useCase)
        {
            var result = await useCase.Execute(request);

            return Created();
        }

        [HttpGet]
        [AuthenticationUser]
        public async Task<IActionResult> GetProfile([FromServices] IGetProfileUseCase useCase)
        {
            var result = await useCase.Execute();

            return Ok(result);
        }

        [HttpPut]
        [AuthenticationUser]
        public async Task<IActionResult> Update([FromBody] RequestUpdateUser request, [FromServices] IUpdateUserUseCase useCase)
        {
            var result = await useCase.Execute(request);

            return Ok(result);
        }

        [AuthenticationUser]
        [HttpPut("update-image")]
        public async Task<IActionResult> UpdateImage(IFormFile file, [FromServices] IUpdateImageUser useCase)
        {
            await useCase.Execute(file);

            return NoContent();
        }

        [HttpPost("verify-code-password")]
        public async Task<IActionResult> VerifyCode([FromBody]RequestVerifyCodePassword request, [FromServices] IVerifyCodePassword useCase)
        {
            var result = await useCase.Execute(request);

            return Ok(result);
        }

        [HttpPost]
        [Route("reset-password")]
        public async Task<IActionResult> ResetPassword([FromQuery]string email, [FromQuery]string token, 
            [FromBody]RequestResetPassword request, [FromServices]IResetPasswordUseCase useCase)
        {
            var result = await useCase.Execute(request, email, token);

            return Ok(result);
        }

        [HttpGet]
        [Route("{user_email}")]
        public async Task<IActionResult> ForgotPassowrd([FromRoute]string user_email, [FromServices]IForgotPasswordUseCase useCase)
        {
            await useCase.Execute(user_email);

            return NoContent();
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail([FromQuery]string email, [FromQuery]string token, [FromServices]IConfirmEmail useCase)
        {
            await useCase.Execute(email, token);

            return NoContent();
        }


        [HttpDelete]
        [AuthenticationUser]
        public async Task<IActionResult> Delete([FromServices] IRequestDeleteAccount useCase)
        {
            await useCase.Execute();

            return NoContent();
        }
    }
}
