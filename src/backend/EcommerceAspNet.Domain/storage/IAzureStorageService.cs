using EcommerceAspNet.Domain.Entitie.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Domain.storage
{
    public interface IAzureStorageService
    {
        public Task Upload(UserEntitie user, Stream file, string fileName);
    }
}
