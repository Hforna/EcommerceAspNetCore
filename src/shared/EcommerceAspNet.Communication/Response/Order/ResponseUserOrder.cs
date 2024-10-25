using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Communication.Response.Order
{
    public class ResponseUserOrder
    {
        public float TotalPrice { get; set; }
        public IList<ResponseCategoryProduct> ProductsCategorys { get; set; }
    }
}
