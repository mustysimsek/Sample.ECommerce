using Sample.ECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ECommerce.Domain.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
    }
}
