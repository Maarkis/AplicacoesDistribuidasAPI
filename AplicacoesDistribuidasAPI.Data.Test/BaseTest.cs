using AplicacoesDistribuidasAPI.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;

namespace AplicacoesDistribuidasAPI.Data.Test
{
    public abstract class BaseTest
    {
        public BaseTest()
        {

        }
      
    }

    public class DbTest : IDisposable
    {
        private string dbName = $"dbApiTest_{Guid.NewGuid().ToString().Replace("-", string.Empty)}";
        public ServiceProvider ServiceProvider { get; private set; }

        public DbTest()
        {
            var serviceColletion = new ServiceCollection();
            serviceColletion.AddDbContext<DataBaseContext>(o =>
                o.UseMySql($"Persist Security Info=True;Server=localhost;database={dbName};uid=root;password=123456"),
                ServiceLifetime.Transient
            );

            ServiceProvider = serviceColletion.BuildServiceProvider();
            using (var context = ServiceProvider.GetService<DataBaseContext>())
            {
                context.Database.EnsureCreated();
            }

        }
        public void Dispose()
        {
            using (var context = ServiceProvider.GetService<DataBaseContext>())
            {
                context.Database.EnsureDeleted();
            }
        }
    }
}
