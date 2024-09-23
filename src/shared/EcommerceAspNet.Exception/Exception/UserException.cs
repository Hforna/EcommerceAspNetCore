using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Exception.Exception
{
    public class UserException : BaseException
    {
        public IList<string> Errors { get; set; } = new List<string>();

        public UserException(string error) : base(error) => Errors.Add(error);

        public UserException(IList<string> errors) : base(string.Empty) => Errors = errors;

        public override HttpStatusCode GetStatusCode() => HttpStatusCode.BadRequest;

        public override string GetErrorMessage() => Message;
    }
}
