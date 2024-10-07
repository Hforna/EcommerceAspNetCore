using EcommerceAspNet.Domain.Entitie.Ecommerce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Domain.Repository.Comment
{
    public interface ICommentReadOnlyRepository
    {
        public Task<IList<CommentEntitie>> CommentByProduct(long? id);
    }
}
