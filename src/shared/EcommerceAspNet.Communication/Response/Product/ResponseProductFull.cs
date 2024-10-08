using EcommerceAspNet.Communication.Response.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Communication.Response.Product
{
    public class ResponseProductFull
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public long Stock { get; set; }
        public string ImageUrl { get; set; }
        public long CategoryId { get; set; }
        public IList<ResponseComment> Comments { get; set; } = [];
    }
}
