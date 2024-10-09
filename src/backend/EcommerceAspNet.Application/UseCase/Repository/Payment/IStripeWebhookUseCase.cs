using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Application.UseCase.Repository.Payment
{
    public interface IStripeWebhookUseCase
    {
        public Task Execute(string jsonBody, string stripeSignature);
    }
}
