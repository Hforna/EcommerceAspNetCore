using Azure.Messaging.ServiceBus;
using EcommerceAspNet.Domain.Entitie.Ecommerce;
using EcommerceAspNet.Domain.Entitie.User;
using EcommerceAspNet.Domain.Repository.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Infrastructure.ServiceBus
{
    public class SendProductUser : ISendDeleteProduct
    { 
        private readonly ServiceBusSender _serviceBusSender;

        public SendProductUser(ServiceBusSender serviceBusSender) => _serviceBusSender = serviceBusSender;

        public async Task SendMessage(Product product)
        {
            await _serviceBusSender.SendMessageAsync(new ServiceBusMessage(product.ProductIdentifier.ToString()));
        }
    }
}
