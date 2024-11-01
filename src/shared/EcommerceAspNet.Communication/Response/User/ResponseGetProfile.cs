﻿using EcommerceAspNet.Communication.Response.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Communication.Response.User
{
    public class ResponseGetProfile
    {
        public string Email { get; set; } = string.Empty;
        public ResponseUserOrder? UserOrder { get; set; }
    }
}
