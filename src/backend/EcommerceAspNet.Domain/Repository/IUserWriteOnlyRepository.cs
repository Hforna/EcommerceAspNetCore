using EcommerceAspNet.Domain.Entitie.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Domain.Repository
{
    public interface IUserWriteOnlyRepository
    {
        public Task Add(UserEntitie user);

        public void Update(UserEntitie user);
    }
}
