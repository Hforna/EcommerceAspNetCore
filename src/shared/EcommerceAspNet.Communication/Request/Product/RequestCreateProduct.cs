using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Communication.Request.Product
{
    public class RequestCreateProduct
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public long Stock { get; set; }
        public IFormFile? ProductImage { get; set; }
        public long CategoryId { get; set; }
    }
}
