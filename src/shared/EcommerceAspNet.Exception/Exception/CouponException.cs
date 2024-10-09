using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Exception.Exception
{
    public class CouponException : BaseException
    {
        private readonly IList<string> Errors = [];

        public CouponException(string error) : base(error) => Errors.Add(error);

        public CouponException(List<string> errors) : base(string.Empty) => Errors = errors;

        public override string GetErrorMessage() => Message;

        public override HttpStatusCode GetStatusCode() => HttpStatusCode.NotFound;
    }
}
