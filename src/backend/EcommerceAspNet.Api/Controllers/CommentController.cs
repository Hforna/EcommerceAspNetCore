using EcommerceAspNet.Api.Attibutes;
using EcommerceAspNet.Application.UseCase.Repository.Comment;
using EcommerceAspNet.Communication.Request.Comment;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAspNet.Api.Controllers
{
    public class CommentController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> CreateComment([FromBody]RequestCreateComment request, [FromServices]ICreateComment useCase)
        {
            var result = await useCase.Execute(request);

            return Created(string.Empty, result);
        }
    }
}
