using EcommerceAspNet.Api.Attibutes;
using EcommerceAspNet.Api.Binders;
using EcommerceAspNet.Application.UseCase.Repository.Product;
using EcommerceAspNet.Communication.Request.Product;
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
        public async Task<IActionResult> UpdateImage(IFormFile file, [FromServices] IUpdateImageProductUseCase useCase, [FromRoute][ModelBinder(typeof(BinderId))]long Id)
        {
            await useCase.Execute(file, Id);

            return NoContent();
        }

        [HttpGet]
        [Authorize(Policy = "CustomerOnly")]
        [Route("{Id}")]
        public async Task<IActionResult> GetProduct([FromRoute][ModelBinder(typeof(BinderId))] long Id, [FromServices] IGetProduct useCase)
        {
            var result = await useCase.Execute(Id);

            return Ok(result);
        }

        [HttpPost]
        [Route("{numberPage}")]
        public async Task<IActionResult> GetProducts([FromServices] IGetProducts useCase, [FromBody]RequestProducts request, [FromRoute]int numberPage)
        {
            var result = await useCase.Execute(request, numberPage);

            return Ok(result);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpDelete]
        [Route("{Id}")]
        public async Task<IActionResult> Delete([FromServices]IRequestDeleteProduct useCase, [FromRoute][ModelBinder(typeof(BinderId))]long Id)
        {
            await useCase.Execute(Id);

            return NoContent();
        }
    }
}
