using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Application.Service.Cryptography
{
    public class PasswordCryptrography : IPasswordCryptography
    {
        public string Encrypt(string password)
        {
            var bytes = Encoding.UTF8.GetBytes(password);
            var encrypt = SHA512.HashData(bytes);

            return ToString(encrypt);
        }

        public string ToString(byte[] bytes)
        {
            var sb = new StringBuilder();

            foreach (var b in bytes)
            {
                var c = b.ToString("X2");
                sb.Append(c);
            }

            return sb.ToString();
        }
    }
}
