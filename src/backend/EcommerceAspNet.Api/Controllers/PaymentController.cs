using EcommerceAspNet.Api.Attibutes;
using EcommerceAspNet.Application.UseCase.Repository.Payment;
using EcommerceAspNet.Communication.Request.Payment;
using EcommerceAspNet.Communication.Response.Payment;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAspNet.Api.Controllers
{
    [AuthenticationUser]
    public class PaymentController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Stripe([FromServices] IStripeUseCase useCase, [FromBody]RequestDiscountCoupon request)
        {
            var result = await useCase.Execute(request);

            return Ok(new ResponseUrlStripe() { Url = result.Url });
        }
    }
}
