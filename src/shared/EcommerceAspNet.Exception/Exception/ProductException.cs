using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Exception.Exception
{
    public class ProductException : BaseException
    {
        public IList<string> Errors { get; set; } = [];

        public ProductException(string error) : base(error) => Errors.Add(error);

        public ProductException(IList<string> errors) : base(string.Empty) => Errors = errors;

        public override string GetErrorMessage() => Message;

        public override HttpStatusCode GetStatusCode() => HttpStatusCode.BadRequest;
    }
}
