using EcommerceAspNet.Api.Attibutes;
using EcommerceAspNet.Application.UseCase.Repository.Payment;
using EcommerceAspNet.Communication.Response.Payment;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAspNet.Api.Controllers
{
    [AuthenticationUser]
    public class PaymentController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> Stripe([FromServices] IStripeUseCase useCase)
        {
            var result = await useCase.Execute();

            //Response.Headers.Add("Location", result.Url);

            return Ok(new ResponseUrlStripe() { Url = result.Url });
        }
    }
}
