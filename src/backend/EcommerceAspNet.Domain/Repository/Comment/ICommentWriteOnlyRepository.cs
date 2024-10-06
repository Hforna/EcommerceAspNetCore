using EcommerceAspNet.Domain.Entitie.Ecommerce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Domain.Repository.Comment
{
    public interface ICommentWriteOnlyRepository
    {
        public void Add(CommentEntitie comment);
    }
}
