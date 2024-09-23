using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Domain.Repository
{
    public interface IUserReadOnlyRepository
    {
        public Task<bool> EmailExists(string email);
    }
}
