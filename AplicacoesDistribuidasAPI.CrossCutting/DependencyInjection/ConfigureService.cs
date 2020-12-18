using AplicacoesDistribuidasAPI.Domain.Interfaces.Services.Product;
using AplicacoesDistribuidasAPI.Service.Services.Product;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace AplicacoesDistribuidasAPI.CrossCutting.DependencyInjection
{
    public class ConfigureService
    {
        public static void ConfigureDependenciesService(IServiceCollection serviceCollection)
        {
            // Services            
            serviceCollection.AddTransient<IProductService, ProductService>();
        }
    }
}
