using EcommerceAspNet.Communication.Request.Comment;
using EcommerceAspNet.Communication.Response.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Application.UseCase.Repository.Comment
{
    public interface ICreateComment
    {
        public Task<ResponseComment> Execute(RequestCreateComment request);
    }
}
