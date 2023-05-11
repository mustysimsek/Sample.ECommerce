using Sample.ECommerce.Domain;
using Sample.ECommerce.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ECommerce.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _appDbContext;
        public UnitOfWork(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                await _appDbContext.SaveChangesAsync(cancellationToken);
            }
            catch (Exception)
            {
                _appDbContext.ChangeTracker.Clear();
                throw;
            }
        }
    }
}
