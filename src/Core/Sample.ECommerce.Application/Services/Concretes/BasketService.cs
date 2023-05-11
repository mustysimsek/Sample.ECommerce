using Mapster;
using Sample.ECommerce.Application.Services.Abstracts;
using Sample.ECommerce.Application.Services.Dto;
using Sample.ECommerce.Domain.Entities;
using Sample.ECommerce.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ECommerce.Application.Services.Concretes
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IProductService _productService;

        public BasketService(IBasketRepository basketRepository, IProductService productService)
        {
            _basketRepository = basketRepository;
            _productService = productService;
        }

        public async Task<Basket> GetBasketAsync(Guid basketId)
        {
            var basket = await _basketRepository.GetBasketAsync(basketId);

            return basket;
        }
        public async Task<Basket> UpdateBasketAsync(BasketViewDto basketViewDto, CancellationToken cancellationToken)
        {
            var basket = await _basketRepository.GetBasketAsync(basketViewDto.Id);
            var itemsInBasket = await _productService.CheckStockQuantityForProducts(basketViewDto.Items, cancellationToken);
            var basketItems = itemsInBasket.Adapt<List<BasketItem>>();
            if (basket is null)
                basket = new() { Id = Guid.NewGuid(), Items = new List<BasketItem>() };
            var existBasketItems = basket.Items.Where(s => basketItems.Any(d => d.Id == s.Id)).ToList();
            foreach (var basketItem in existBasketItems)
            {
                var product = basketItems.Single(s => s.Id == basketItem.Id);
                basketItem.Quantity += product.Quantity;
            }
            var newItems = basketItems.Where(s => !existBasketItems.Any(d => d.Id == s.Id));
            basket.Items.AddRange(newItems);
            return await _basketRepository.UpdateBasketAsync(basket);
        }
        public async Task DeleteBasketAsync(Guid basketId)
        {
            await _basketRepository.DeleteBasketAsync(basketId);
        }
    }
}
