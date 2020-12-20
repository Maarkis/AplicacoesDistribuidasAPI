using AplicacoesDistribuidasAPI.Data.Mapping;
using AplicacoesDistribuidasAPI.Data.Mapping.User;
using AplicacoesDistribuidasAPI.Domain.Entities;
using AplicacoesDistribuidasAPI.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

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
            modelBuilder.Entity<UserEntity>(new UserMap().Configure);
        }

    }
}
