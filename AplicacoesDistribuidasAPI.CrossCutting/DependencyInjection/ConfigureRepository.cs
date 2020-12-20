using AplicacoesDistribuidasAPI.Data.Context;
using AplicacoesDistribuidasAPI.Data.Repository;
using AplicacoesDistribuidasAPI.Data.Repository.User;
using AplicacoesDistribuidasAPI.Domain.Interfaces;
using AplicacoesDistribuidasAPI.Domain.Repository;
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
            serviceCollection.AddTransient<IUserRepository, UserRepository>();
            //serviceCollection.AddScoped<IProductRepository, ProductRepository>();
        }
    }
}