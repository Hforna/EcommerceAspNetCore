using EcommerceAspNet.Application.UseCase.Repository.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAspNet.Api.Controllers
{
    public class ProductController : BaseController
    {
        [HttpPut]
        [Route("{Id}")]
        public async Task<IActionResult> UpdateImage(IFormFile file, [FromServices] IUpdateImageProductUseCase useCase, long Id)
        {
            await useCase.Execute(file, Id);

            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {

        }
    }
}
