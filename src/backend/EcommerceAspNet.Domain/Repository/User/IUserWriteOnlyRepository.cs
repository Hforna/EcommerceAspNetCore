using EcommerceAspNet.Domain.Entitie.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Domain.Repository.User
{
    public interface IUserWriteOnlyRepository
    {
        public Task Add(Entitie.User.User user);

        public void Update(Entitie.User.User user);

        public Task Delete(Guid uid);
    }
}
