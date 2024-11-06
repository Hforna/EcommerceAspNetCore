using Azure.Messaging.ServiceBus;
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
        public SendDeleteUser(ServiceBusSender serviceBus) => _serviceBusSender = serviceBus;
        public async Task SendMessage(Guid uid)
        {
            var message = new ServiceBusMessage(uid.ToString());
            message.ApplicationProperties["MessageType"] = "DeleteUser";

            await _serviceBusSender.SendMessageAsync(message);
        }
    }
}
