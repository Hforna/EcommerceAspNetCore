using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Application.Service.Cryptography
{
    public interface IPasswordCryptography
    {
        public string Encrypt(string password);
    }
}
