using EcommerceAspNet.Domain.Entitie.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Domain.Entitie.Ecommerce
{
    public class CommentEntitie : BaseEntitie
    {
        public string Text { get; set; } = string.Empty;
        public Int16 Note {  get; set; }
        public long ProductId { get; set; }
        public long? UserId { get; set; }
        public UserEntitie User { get; set; }
    }
}
