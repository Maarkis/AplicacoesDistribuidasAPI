using AplicacoesDistribuidasAPI.Data.Context;
using AplicacoesDistribuidasAPI.Data.Repository;
using AplicacoesDistribuidasAPI.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace AplicacoesDistribuidasAPI.CrossCutting.DependencyInjection
{
    public class ConfigureRepository
    {
        public static void ConfigureDependenciesRepository(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient(typeof(IRepository<>), typeof(BaseRepository<>));
            //serviceCollection.AddScoped<IProductRepository, ProductRepository>();
        }
    }
}