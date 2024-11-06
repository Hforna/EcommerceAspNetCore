using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Exception.Exception
{
    public class OrderException : BaseException
    {
        public OrderException(string error) : base(error)
        {
        }

        public OrderException(IList<string> errors) : base(string.Empty) => Errors = errors;

        public IList<string> Errors { get; set; }

        public override string GetErrorMessage() => Message;

        public override HttpStatusCode GetStatusCode() => HttpStatusCode.BadRequest;
    }
}
