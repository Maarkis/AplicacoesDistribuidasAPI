using AplicacoesDistribuidasAPI.Data.Mapping;
using AplicacoesDistribuidasAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AplicacoesDistribuidasAPI.Data.Context
{
    public class DataBaseContext : DbContext
    {
        public DbSet<ProductEntity> Products { get; set; }

        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductEntity>(new ProductMap().Configure);
        }

    }
}
