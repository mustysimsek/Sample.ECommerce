using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sample.ECommerce.Application.Services.Abstracts;
using Sample.ECommerce.Application.Services.Dto;
using Sample.ECommerce.Domain.Entities;

namespace Sample.ECommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;
        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Basket>> GetBasket(Guid id)
        {
            var basket = await _basketService.GetBasketAsync(id);

            return Ok(basket);
        }

        [HttpPost]
        public async Task<ActionResult<Basket>> UpdateBasket(BasketViewDto basketViewDto,CancellationToken cancellationToken)
        {
            var updatedBasket = await _basketService.UpdateBasketAsync(basketViewDto, cancellationToken);

            return Ok(updatedBasket);
        }

        [HttpDelete]
        public async Task DeleteBasket(Guid id)
        {
            await _basketService.DeleteBasketAsync(id);
        }
    }
}
