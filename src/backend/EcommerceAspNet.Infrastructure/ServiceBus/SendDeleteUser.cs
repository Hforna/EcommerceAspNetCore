using Azure.Messaging.ServiceBus;
using EcommerceAspNet.Domain.Entitie.User;
using EcommerceAspNet.Domain.Repository.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Infrastructure.ServiceBus
{
    public class SendDeleteUser : ISendDeleteUser
    { 
        private readonly ServiceBusSender _serviceBusSender;

        public SendDeleteUser(ServiceBusSender serviceBusSender) => _serviceBusSender = serviceBusSender;

        public async Task SendMessage(UserEntitie user)
        {
            await _serviceBusSender.SendMessageAsync(new ServiceBusMessage(user.UserIdentifier.ToString()));
        }
    }
}
