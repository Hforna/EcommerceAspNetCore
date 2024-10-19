using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Communication.Request.User
{
    public class RequestResetPassword
    {
        public required string Password { get; set; }
        public required string RepeatPassword { get; set; }
    }
}
