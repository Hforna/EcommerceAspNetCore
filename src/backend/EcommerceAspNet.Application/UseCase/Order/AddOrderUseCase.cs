using AutoMapper;
using EcommerceAspNet.Application.UseCase.Repository.Order;
using EcommerceAspNet.Communication.Response.Order;
using EcommerceAspNet.Communication.Response.Product;
using EcommerceAspNet.Domain.Entitie.Ecommerce;
using EcommerceAspNet.Domain.Repository;
using EcommerceAspNet.Domain.Repository.Order;
using EcommerceAspNet.Domain.Repository.Product;
using EcommerceAspNet.Domain.Repository.Security;
using EcommerceAspNet.Exception.Exception;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Sqids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EcommerceAspNet.Application.UseCase.Order
{
    public class AddOrderUseCase : IAddOrderUseCase
    {
        private readonly IProductReadOnlyRepository _repositoryProductRead;
        private readonly IOrderReadOnlyRepository _repositoryOrderRead;
        private readonly IOrderWriteOnlyRepository _repositoryOrderWrite;
        private readonly IGetUserByToken _userLogged;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly SqidsEncoder<long> _sqidsEncoder;

        public AddOrderUseCase(IProductReadOnlyRepository repositoryProductRead, IOrderReadOnlyRepository repositoryOrderRead, 
            IOrderWriteOnlyRepository repositoryOrderWrite, 
            IGetUserByToken userLogged, IUnitOfWork unitOfWork, 
            IMapper mapper, SqidsEncoder<long> sqidsEncoder)
        {
            _repositoryProductRead = repositoryProductRead;
            _repositoryOrderRead = repositoryOrderRead;
            _repositoryOrderWrite = repositoryOrderWrite;
            _userLogged = userLogged;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _sqidsEncoder = sqidsEncoder;
        }

        public async Task<ResponseUserOrder> Execute(long id, ISession session)
        {
            var product = await _repositoryProductRead.ProductById(id);
            
            if (product is null)
                throw new ProductException("Product doesn't exists");

            if (product.Stock < 1)
                throw new ProductException("Product is out of stock");

            var user = await _userLogged.GetUser();

            var orderUser = new Domain.Entitie.Ecommerce.Order();

            if (user is null)
            {
                var productId = _sqidsEncoder.Encode(id);

                var orderItemList = new List<string>();

                var order = new Domain.Entitie.Ecommerce.Order();

                if (session.TryGetValue("order", out var type))
                {
                    var orderItemSession = session.Get("orderItems");

                    orderItemList = System.Text.Json.JsonSerializer.Deserialize<List<string>>(orderItemSession)!;
                    orderItemList.Add(productId);

                    var productOrderItem = new List<OrderItemEntitie>();

                    var productsList = orderItemList.Select(async orderItem =>
                    {
                        var product = await _repositoryProductRead.ProductById(long.Parse(orderItem));
                        productOrderItem.Add(new OrderItemEntitie()
                        {
                            Quantity = 1,
                            UnitPrice = product.Price,
                            Name = product.Name,
                            productId = product.Id
                        });

                        return product;
                    }).ToList();

                    var productsListTask = await Task.WhenAll(productsList);

                    orderUser.OrderItems = productOrderItem;
                } else
                {
                    var orderItemSession = session.Get("orderItems");
                    session.SetString("order", session.Id);

                    orderItemList = new List<String>();
                    orderItemList.Add(productId);

                    var orderItemObject = new OrderItemEntitie() { productId = id, Product = product, Quantity = 1, UnitPrice = product.Price};

                    order = new Domain.Entitie.Ecommerce.Order() { OrderItems = new List<OrderItemEntitie>() { orderItemObject } };
                }
                orderUser = order;

                var converToString = JsonConvert.SerializeObject(orderItemList);
                session.SetString("orderItems", System.Text.Json.JsonSerializer.Serialize<string>(converToString));
            }

            if (user is not null)
                orderUser = await _repositoryOrderRead.UserOrder(user!);

            float totalPrice = 0;

            if (orderUser is null && user is not null)
            {
                orderUser = new Domain.Entitie.Ecommerce.Order() { UserId = user!.Id };

                var orderItems = new OrderItemEntitie()
                {
                    orderId = orderUser!.Id,
                    productId = product.Id,
                    Quantity = 1,
                    UnitPrice = product.Price,
                    Name = product.Name
                };

                _repositoryOrderWrite.AddOrderItem(orderItems);
                _repositoryOrderWrite.AddOrder(orderUser);

                await _unitOfWork.Commit();

                orderUser = await _repositoryOrderRead.OrderItemList(orderUser);

                totalPrice = 0;

                foreach (var item in orderUser!.OrderItems)
                {
                    totalPrice += item!.UnitPrice;
                }

                orderUser.TotalPrice = totalPrice;

                var descountIfSumMethod = descountIfSumOfPerferct(totalPrice);

                if (descountIfSumMethod.isDescountValid)
                {
                    orderUser.TotalPrice -= descountIfSumMethod.totalPriceDescount;
                }

            } else
            {
                totalPrice = 0;

                foreach (var item in orderUser!.OrderItems)
                {
                    totalPrice += item!.UnitPrice;
                }

                orderUser.TotalPrice = totalPrice;

                var descountIfSumMethod = descountIfSumOfPerferct(totalPrice);

                if (descountIfSumMethod.isDescountValid)
                {
                    orderUser.TotalPrice -= descountIfSumMethod.totalPriceDescount;
                }

                return _mapper.Map<ResponseUserOrder>(orderUser);
            }

            _repositoryOrderWrite.UpdateOrder(orderUser);
            await _unitOfWork.Commit();

            return _mapper.Map<ResponseUserOrder>(orderUser);
        }

        private (bool isDescountValid, float totalPriceDescount) descountIfSumOfPerferct(float totalPrice)
        {
            var sumOfPerfectNum = 0.00;

            var totalPriceInt = (int)totalPrice;

            for (int i = 1; i < totalPriceInt; i++)
            {
                if (totalPriceInt % i == 0)
                    sumOfPerfectNum += i;
            }

            return (sumOfPerfectNum == totalPriceInt, totalPrice * 0.05f);
        }
    }
}
