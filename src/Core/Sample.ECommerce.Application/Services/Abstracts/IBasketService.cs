using Sample.ECommerce.Application.Services.Dto;
using Sample.ECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ECommerce.Application.Services.Abstracts
{
    public interface IBasketService
    {
        Task<Basket> GetBasketAsync(Guid basketId);
        Task<Basket> UpdateBasketAsync(BasketViewDto basket, CancellationToken cancellationToken);
        Task DeleteBasketAsync(Guid basketId);
    }
}
