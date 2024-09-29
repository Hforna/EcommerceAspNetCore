using Azure.Messaging.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Infrastructure.ServiceBus
{
    public class DeleteUserProcessor
    {
        private readonly ServiceBusProcessor _busProcessor;

        public DeleteUserProcessor(ServiceBusProcessor busProcessor) => _busProcessor = busProcessor;

        public ServiceBusProcessor GetProcessor() => _busProcessor;
    }
}
