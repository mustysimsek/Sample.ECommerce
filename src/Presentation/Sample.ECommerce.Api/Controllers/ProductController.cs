using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sample.ECommerce.Application.Services.Abstracts;

namespace Sample.ECommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productRepository)
        {
            _productService = productRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var list = await _productService.GetProductsAsync(cancellationToken);
            return Ok(list);
        }
    }
}
