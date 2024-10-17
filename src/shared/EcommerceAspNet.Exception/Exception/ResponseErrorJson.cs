using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Exception.Exception
{
    public class ResponseErrorJson : BaseException
    {
        public IList<string> Errors { get; set; } = [];

        public ResponseErrorJson(IList<string> errors) : base(string.Empty) => Errors = errors;

        public ResponseErrorJson(string error) : base(error) => Errors.Add(error);

        public override HttpStatusCode GetStatusCode() => HttpStatusCode.BadRequest;

        public override string GetErrorMessage() => Message;
    }
}
