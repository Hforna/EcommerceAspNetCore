using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Communication.Response.Product
{
    public class ResponseProductDuo
    {
        public ResponseProductShort FirstProduct { get; set; }
        public ResponseProductShort SecondProduct { get; set; }
    }
}
