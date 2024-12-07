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
        [ProducesResponseType(typeof(ResponseOrderItem), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddProductToOrder([FromServices] IAddOrderUseCase useCase, [FromRoute][ModelBinder(typeof(BinderId))] long Id)
        {
            var session = HttpContext.Session;
            var result = await useCase.Execute(Id, session);

            return Ok(result);
        }

        [HttpPut]
        [Route("{Id}/{Quantity}")]
        [AuthenticationUser]
        public async Task<IActionResult> UpdateQuantity([FromRoute][ModelBinder(typeof(BinderId))]long Id, [FromRoute]int Quantity, [FromServices]IUpdateQuantityUseCase useCase)
        {
            await useCase.Execute(Id, Quantity);

            return NoContent();
        }

        [HttpGet]
        [AuthenticationUser]
        public async Task<IActionResult> GetOrder([FromServices]IGetOrderUseCase useCase)
        {
            var result = await useCase.Execute();

            return Ok(result);
        }

        [AuthenticationUser]
        [HttpGet("order-history")]
        public async Task<IActionResult> GetOrderHistory([FromServices]IGetHistoryOrders useCase)
        {
            var result = await useCase.Execute();

            return Ok(result);
        }

        [AuthenticationUser]
        [HttpDelete]
        [Route("{Id}")]
        public async Task<IActionResult> DeleteProductFromOrder([FromServices]IDeleteOrderItemUseCase useCase, [FromRoute][ModelBinder(typeof(BinderId))]long Id)
        {
            await useCase.Execute(Id);

            return NoContent();
        }
    }
}
