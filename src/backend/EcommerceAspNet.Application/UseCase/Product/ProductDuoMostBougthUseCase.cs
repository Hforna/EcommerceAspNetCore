using AutoMapper;
using EcommerceAspNet.Application.UseCase.Repository.Product;
using EcommerceAspNet.Communication.Response.Product;
using EcommerceAspNet.Domain.Entitie.Ecommerce;
using EcommerceAspNet.Domain.Repository.Order;
using EcommerceAspNet.Domain.Repository.Product;
using EcommerceAspNet.Domain.Repository.Storage;
using Sqids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Application.UseCase.Product
{
    public class ProductDuoMostBougthUseCase : IProductDuoMostBougth
    {
        private readonly IOrderReadOnlyRepository _orderReadOnly;
        private readonly IMapper _mapper;
        private readonly IAzureStorageService _azureStorage;
        private readonly IProductReadOnlyRepository _productReadOnly;
        private readonly SqidsEncoder<long> _sqidsEncoder;

        public ProductDuoMostBougthUseCase(IOrderReadOnlyRepository _orderReadOnlyRepository, IMapper mapper, 
            IAzureStorageService azureStorage, IProductReadOnlyRepository productReadOnly,
            SqidsEncoder<long> sqidsEncoder)
        {
            _orderReadOnly = _orderReadOnlyRepository;
            _mapper = mapper;
            _azureStorage = azureStorage;
            _productReadOnly = productReadOnly;
            _sqidsEncoder = sqidsEncoder;
        }

        public async Task<List<ResponseProductDuo>> Execute(int qtyProduct)
        {
            List<List<EcommerceAspNet.Domain.Entitie.Ecommerce.Product>> duoProducts = new List<List<Domain.Entitie.Ecommerce.Product>>();
            Dictionary<(long, long), long> productDictionary = new Dictionary<(long, long), long>();

            var orders = await _orderReadOnly.OrdersNotActive();

            foreach (var order in orders)
            {
                for (var i = 0; i < order.OrderItems.Count; i++)
                {
                    for (var j = 0; j < order.OrderItems.Count; j++)
                    {
                        var productPair = (Math.Min(order.OrderItems[i].productId, order.OrderItems[j].productId),
                                   Math.Max(order.OrderItems[i].productId, order.OrderItems[j].productId));

                        if (productDictionary.ContainsKey(productPair))
                        {
                            productDictionary[productPair] += 1;
                        } else
                        {
                            productDictionary[productPair] = 1;
                        }
                    }
                }
            }

            var productOrdered = productDictionary.OrderBy(d => d.Value).Take(qtyProduct);

            var responseList = new List<ResponseProductDuo>();

            foreach (var product in productOrdered)
            {
                var responseDuo = new ResponseProductDuo();
                var responseShort = new List<ResponseProductShort>();

                var product1 = await _productReadOnly.ProductById(product.Key.Item1);
                var response1 = _mapper.Map<ResponseProductShort>(product1);
                response1.Id = _sqidsEncoder.Encode(product1.Id);
                response1.ImageUrl = await _azureStorage.GetUrlImageProduct(product1, product1.ImageIdentifier);

                var product2 = await _productReadOnly.ProductById(product.Key.Item2);
                var response2 = _mapper.Map<ResponseProductShort>(product2);
                response2.Id = _sqidsEncoder.Encode(product2.Id);
                response2.ImageUrl = await _azureStorage.GetUrlImageProduct(product2, product2.ImageIdentifier);

                responseShort.Add(response1);
                responseShort.Add(response2);

                responseDuo.FirstProduct = responseShort[0];
                responseDuo.SecondProduct = responseShort[1];
                responseList.Add(responseDuo);
            };

            return responseList;
        }
    }
}
