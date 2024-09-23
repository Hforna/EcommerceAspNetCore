using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Exception.Exception
{
    public abstract class BaseException : SystemException
    {
        public BaseException(string error) : base(error) { }

        public abstract HttpStatusCode GetStatusCode();       
        public abstract string GetErrorMessage();
    }
}
