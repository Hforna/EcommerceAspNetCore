using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Communication.Response.Product
{
    public class ResponseProductShort
    {
        public string Name {  get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public float Price { get; set; }
        public long Id { get; set; }
    }
}
