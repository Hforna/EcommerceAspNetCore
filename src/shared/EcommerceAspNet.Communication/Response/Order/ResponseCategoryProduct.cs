using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Communication.Response.Order
{
    public class ResponseCategoryProduct
    {
        public string CategoryName { get; set; }
        public string CategoryId { get; set; }
        public IList<ResponseOrderItem> Products { get; set; }
    }
}
