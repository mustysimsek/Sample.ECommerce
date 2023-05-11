using Microsoft.EntityFrameworkCore;
using Sample.ECommerce.Application.Interfaces;
using Sample.ECommerce.Domain.Common;
using Sample.ECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ECommerce.Persistence.Context
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            this.ChangeTracker.DetectChanges();
            foreach (var entity in this.ChangeTracker.Entries<BaseEntity>().Where(i => i.State == EntityState.Added))
            {
                entity.Entity.CreateDate = DateTime.Now;
                entity.Entity.ModifiedDate = DateTime.Now;
                entity.Entity.SetIsActive(true);
            }
            foreach (var entity in this.ChangeTracker.Entries<BaseEntity>().Where(i => i.State == EntityState.Modified))
            {
                entity.Entity.ModifiedDate = DateTime.Now;
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
