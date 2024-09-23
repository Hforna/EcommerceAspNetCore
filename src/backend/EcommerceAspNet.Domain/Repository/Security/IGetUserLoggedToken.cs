using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Domain.Repository.Security
{
    public interface IGetUserLoggedToken
    {
        public string GetToken();
    }
}
