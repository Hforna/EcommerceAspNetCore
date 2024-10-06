using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Application.UseCase.Repository.User
{
    public interface IUpdateImageUser
    {
        public Task Execute(IFormFile file);
    }
}
