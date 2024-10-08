using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Communication.Response.Order
{
    public class ResponseOrderItem
    {
        public string Name { get; set; } = string.Empty;
        public float UnitPrice { get; set; }
        public int Quantity { get; set; }
        public string Id { get; set; }
    }
}
