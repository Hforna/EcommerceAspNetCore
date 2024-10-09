using EcommerceAspNet.Api.Attibutes;
using EcommerceAspNet.Application.UseCase.Repository.Payment;
using EcommerceAspNet.Communication.Request.Payment;
using EcommerceAspNet.Communication.Response.Payment;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace EcommerceAspNet.Api.Controllers
{
    public class PaymentController : BaseController
    {
        [AuthenticationUser]
        [HttpPost]
        public async Task<IActionResult> Stripe([FromServices] IStripeUseCase useCase, [FromBody]RequestDiscountCoupon request)
        {
            var result = await useCase.Execute(request);

            return Ok(new ResponseUrlStripe() { Url = result.Url });
        }

        [HttpPost("webhook")]
        public async Task<IActionResult> Webhook([FromServices]IStripeWebhookUseCase useCase)
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            await useCase.Execute(json, Request.Headers["Stripe-Signature"]!);

            return Ok();
        }
    }
}
