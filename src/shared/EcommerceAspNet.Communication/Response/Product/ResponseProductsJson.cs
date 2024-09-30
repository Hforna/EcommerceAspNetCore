using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Communication.Response.Product
{
    public class ResponseProductsJson
    {
        public IList<ResponseProductShort> Products { get; set; } = [];
    }
}
