using EcommerceAspNet.Communication.Request.User;
using EcommerceAspNet.Communication.Response.User;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Application.UseCase.Repository.User
{
    public interface IVerifyCodePassword
    {
        public Task<ResponseUserResetPassword> Execute(RequestVerifyCodePassword request);
    }
}
