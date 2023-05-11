using Mapster;
using Sample.ECommerce.Application.Exceptions;
using Sample.ECommerce.Application.Services.Abstracts;
using Sample.ECommerce.Application.Services.Dto;
using Sample.ECommerce.Domain.Entities;
using Sample.ECommerce.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ECommerce.Application.Services.Concretes
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<List<ProductViewDto>> GetProductsAsync(CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAllAsync(cancellationToken);
            return products.Adapt<List<ProductViewDto>>();
        }
        public async Task<List<ProductViewDto>> CheckStockQuantityForProducts(List<BasketItemViewDto> basketItems, CancellationToken cancellationToken)
        {
            #region Expression<Func<T, bool>> Ornek
            //Expression<Func<int, bool>> isEvenExpression = x => x % 2 == 0;
            //List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6 };
            //var evenNumbers = numbers.Where(isEvenExpression.Compile());

            //Yukarıdaki örnekte, isEvenExpression ifadesi, bir sayının çift olup olmadığını kontrol eden bir ifadeyi temsil eder.
            //Bu ifade, Where metodu tarafından kullanılarak numbers koleksiyonunu filtrelemek için kullanılır. 
            //isEvenExpression.Compile() ifadesi, Expression<Func<int, bool>> ifadesini çalıştırılabilir bir fonksiyon olan Func<int, bool>'a dönüştürür.
            //Böylece, Where metodu bu filtre fonksiyonunu kullanabilir.
            #endregion

            var products = await _productRepository.GetAllAsync(i => basketItems.Select(i => i.ProductId).Contains(i.Id), cancellationToken);
            if (!products.Any())
                throw new AppExtension($"There is no product in this ID.");

            var result = new List<ProductViewDto>();
            foreach (var item in basketItems)
            {
                var selectedProduct = products.SingleOrDefault(x => x.Id == item.ProductId);
                if (selectedProduct is not null)
                {
                    //checking stock
                    if (item.ProductQuantity > selectedProduct.StockQuantity)
                    {
                        throw new AppExtension($"There is not enough stock with the ID :'{item.ProductId}'");
                    }

                    var resultItem = selectedProduct.Adapt<ProductViewDto>();
                    resultItem.Quantity = item.ProductQuantity;
                    resultItem.Price = selectedProduct.Price;
                    result.Add(resultItem);
                }
            }
            return result;
        }
    }
}
