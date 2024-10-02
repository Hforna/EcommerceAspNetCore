using EcommerceAspNet.Api.Attibutes;
using EcommerceAspNet.Application.UseCase.Repository.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAspNet.Api.Controllers
{
    public class ProductController : BaseController
    {
        [AuthenticationUser]
        [HttpPut]
        [Route("{Id}")]
        public async Task<IActionResult> UpdateImage(IFormFile file, [FromServices] IUpdateImageProductUseCase useCase, long Id)
        {
            await useCase.Execute(file, Id);

            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts([FromServices] IGetProducts useCase)
        {
            var result = await useCase.Execute();

            return Ok(result);
        }

        [AuthenticationUser]
        [HttpDelete]
        [Route("Id")]
        public async Task<IActionResult> Delete([FromServices]IRequestDeleteProduct useCase, long Id)
        {
            await useCase.Execute(Id);

            return NoContent();
        }
    }
}
