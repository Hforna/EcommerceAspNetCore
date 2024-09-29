using EcommerceAspNet.Api.Attibutes;
using EcommerceAspNet.Application.UseCase.Repositorie.User;
using EcommerceAspNet.Application.UseCase.Repository.User;
using EcommerceAspNet.Communication.Request.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAspNet.Api.Controllers
{
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

        [HttpDelete]
        [AuthenticationUser]
        public async Task<IActionResult> Delete([FromServices] IRequestDeleteAccount useCase)
        {
            await useCase.Execute();

            return NoContent();
        }
    }
}
