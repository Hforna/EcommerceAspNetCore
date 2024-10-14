using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Communication.Request.Product
{
    public class RequestProducts
    {
        public long? CategoryId { get; set; }
        public int? price { get; set; }
    }
}
