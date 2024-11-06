using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Domain.Repository.ServiceBus
{
    public interface ISendDeleteUser
    {
        public Task SendMessage(Guid uid);
    }
}
