using EcommerceAspNet.Api.Attibutes;
using EcommerceAspNet.Api.Binders;
using EcommerceAspNet.Application.UseCase.Repository.Order;
using EcommerceAspNet.Communication.Response.Order;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAspNet.Api.Controllers
{
    public class OrderController : BaseController
    {
        [HttpPost]
        [Route("{Id}")]
        [AuthenticationUser]
        [ProducesResponseType(typeof(ResponseOrderItem), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddProduct([FromServices] IAddOrderUseCase useCase, [FromRoute][ModelBinder(typeof(BinderId))] long Id)
        {
            var result = await useCase.Execute(Id);

            return Ok(result);
        }

        [HttpDelete]
        [Route("{Id}")]
        public async Task<IActionResult> DeleteProduct([FromServices]IDeleteOrderItemUseCase useCase, [FromRoute][ModelBinder(typeof(BinderId))]long Id)
        {
            await useCase.Execute(Id);

            return NoContent();
        }
    }
}
