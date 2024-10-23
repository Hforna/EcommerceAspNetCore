using EcommerceAspNet.Api.Attibutes;
using EcommerceAspNet.Api.Binders;
using EcommerceAspNet.Application.UseCase.Repository.Product;
using EcommerceAspNet.Communication.Request.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace EcommerceAspNet.Api.Controllers
{
    public class ProductController : BaseController
    {
        [DisableCors]
        [HttpPost("create-product")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> CreateProduct([FromForm] RequestCreateProduct request, [FromServices] ICreateProductUseCase useCase)
        {
            var result = await useCase.Execute(request);

            return Created(string.Empty, result);
        }

        [AuthenticationUser]
        [HttpPut]
        [Route("{Id}")]
        public async Task<IActionResult> UpdateImage(IFormFile file, [FromServices] IUpdateImageProductUseCase useCase, [FromRoute][ModelBinder(typeof(BinderId))]long Id)
        {
            await useCase.Execute(file, Id);

            return NoContent();
        }

        [HttpGet]
        [Route("{Id}")]
        public async Task<IActionResult> GetProduct([FromRoute][ModelBinder(typeof(BinderId))] long Id, [FromServices] IGetProduct useCase)
        {
            var result = await useCase.Execute(Id);

            return Ok(result);
        }

        [HttpPost]
        [EnableRateLimiting("getallproductslimiter")]
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
