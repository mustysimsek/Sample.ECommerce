using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ECommerce.Application.Services.Dto
{
    public class BasketViewDto
    {
        public Guid Id { get; set; }
        public List<BasketItemViewDto> Items { get; set; } = new List<BasketItemViewDto>();
    }
}
