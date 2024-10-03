using BCrypt.Net;
using EcommerceAspNet.Domain.Repository.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Infrastructure.Security.Cryptography
{
    public class BCryptNet : IPasswordCryptography
    {
        public string Encrypt(string password)
        {
           return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool IsValid(string password, string hashPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashPassword);
        }
    }
}
