using Sample.ECommerce.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ECommerce.Application.Services.Abstracts
{
    public interface IProductService
    {
        Task<List<ProductViewDto>> GetProductsAsync(CancellationToken cancellationToken);
        Task<List<ProductViewDto>> CheckStockQuantityForProducts(List<BasketItemViewDto> basketItems, CancellationToken cancellationToken);
    }
}
