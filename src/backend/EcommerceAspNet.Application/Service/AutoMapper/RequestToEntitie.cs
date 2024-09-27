using AutoMapper;
using EcommerceAspNet.Communication.Request.User;
using EcommerceAspNet.Domain.Entitie.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Application.Service.AutoMapper
{
    public class RequestToEntitie : Profile
    {
        public RequestToEntitie()
        {
            CreateMap<RequestCreateUser, UserEntitie>()
                .ForMember(u => u.Password, f => f.Ignore());

            CreateMap<RequestUpdateUser, UserEntitie>()
                .ForMember(u => u.Password, (f) => f.Ignore());
        }
    }
}
