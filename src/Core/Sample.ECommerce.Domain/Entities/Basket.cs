using Sample.ECommerce.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ECommerce.Domain.Entities
{
    public class Basket
    {
        public Guid Id { get; set; }
        public List<BasketItem> Items { get; set; }
        public decimal TotalPrice
        {
            get
            {
                return Items.Sum(i => i.TotalPrice);
            }
        }
    }
}
