﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Domain.Repository
{
    public interface IUnitOfWork
    {
        public Task Commit();
    }
}
