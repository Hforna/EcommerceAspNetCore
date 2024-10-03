using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Domain.Repository.Security.Cryptography
{
    public interface IPasswordCryptography
    {
        public string Encrypt(string password);
        public bool IsValid(string password, string hashPassword);
    }
}
