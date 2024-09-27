using EcommerceAspNet.Domain.Entitie.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Domain.Repository.Security
{
    public interface IGetUserByToken
    {
        public Task<UserEntitie?> GetUser();
    }
}
