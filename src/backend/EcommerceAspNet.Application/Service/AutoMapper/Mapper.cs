using AutoMapper;
using EcommerceAspNet.Communication.Request.User;
using EcommerceAspNet.Communication.Response.Product;
using EcommerceAspNet.Domain.Entitie.Ecommerce;
using EcommerceAspNet.Domain.Entitie.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Application.Service.AutoMapper
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            RequestToEntitie();
            EntitieToResponse();
        }

        public void RequestToEntitie()
        {
            CreateMap<RequestCreateUser, UserEntitie>()
                .ForMember(u => u.Password, f => f.Ignore());

            CreateMap<RequestUpdateUser, UserEntitie>()
                .ForMember(u => u.Password, (f) => f.Ignore());
        }

        public void EntitieToResponse()
        {
            CreateMap<ProductEntitie, ResponseProductShort>()
                .ForMember(d => d.ImageUrl, f => f.Ignore())
                .ForMember(d => d.Id, f => f.Ignore());
        }
    }
}
