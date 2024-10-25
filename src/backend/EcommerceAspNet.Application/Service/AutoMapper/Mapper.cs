using AutoMapper;
using EcommerceAspNet.Communication.Request.Comment;
using EcommerceAspNet.Communication.Request.Product;
using EcommerceAspNet.Communication.Request.User;
using EcommerceAspNet.Communication.Response.Comment;
using EcommerceAspNet.Communication.Response.Order;
using EcommerceAspNet.Communication.Response.Product;
using EcommerceAspNet.Domain.Entitie.Ecommerce;
using EcommerceAspNet.Domain.Entitie.User;
using PagedList;
using Sqids;
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
        private readonly SqidsEncoder<long> _sqids;
        public Mapper(SqidsEncoder<long> sqids)
        {
            _sqids = sqids;

            RequestToEntitie();
            EntitieToResponse();
        }

        public void RequestToEntitie()
        {
            CreateMap<RequestCreateUser, UserEntitie>()
                .ForMember(u => u.Password, f => f.Ignore());

            CreateMap<RequestUpdateUser, UserEntitie>()
                .ForMember(u => u.Password, (f) => f.Ignore());

            CreateMap<RequestCreateProduct, Product>()
                .ForMember(d => d.ImageIdentifier, (f) => f.Ignore());

            CreateMap<RequestCreateComment, CommentEntitie>()
                .ForMember(d => d.ProductId, (f) => f.MapFrom(d => _sqids.Decode(d.ProductId).Single()));
        }

        public void EntitieToResponse()
        {
            CreateMap<Product, ResponseProductShort>()
                .ForMember(d => d.ImageUrl, f => f.Ignore())
                .ForMember(d => d.Id, f => f.Ignore());

            CreateMap<Order, ResponseUserOrder>()
                .ForMember(d => d.TotalPrice, f => f.MapFrom(d => d.TotalPrice));

            CreateMap<CommentEntitie, ResponseComment>()
                .ForMember(d => d.Username, f => f.MapFrom(d => d.User.UserName));

            CreateMap<Product, ResponseProductFull>();

            CreateMap<Dictionary<(string CategoryName, long CategoryId), IList<EcommerceAspNet.Domain.Entitie.Ecommerce.OrderItemEntitie>>, List<ResponseCategoryProduct>>()
                .ConvertUsing(d => d.Select(d => new ResponseCategoryProduct()
                {
                    CategoryId = _sqids.Encode(d.Key.CategoryId),
                    CategoryName = d.Key.CategoryName,
                    Products = d.Value.Select(p => new ResponseOrderItem()
                    {
                        Id = _sqids.Encode(p.Id),
                        Name = p.Name,
                        Quantity = p.Quantity,
                        UnitPrice = p.UnitPrice,
                    }).ToList()
                }).ToList());

            CreateMap<OrderItemEntitie, ResponseOrderItem>()
                .ForMember(d => d.Id, f => f.MapFrom(d => _sqids.Encode(d.Id)));                
        }
    }
}
