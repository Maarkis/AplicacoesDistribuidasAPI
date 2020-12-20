using AplicacoesDistribuidasAPI.Domain.Interfaces.Services.Product;
using AplicacoesDistribuidasAPI.Domain.Interfaces.Services.User;
using AplicacoesDistribuidasAPI.Service.Services.Product;
using AplicacoesDistribuidasAPI.Service.Services.User;
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
            serviceCollection.AddTransient<IUserService, UserService>();
            serviceCollection.AddTransient<IAuthenticationService, AuthenticationService>();

            serviceCollection.AddCors(options =>
            {
                options.AddPolicy("MyAllowSpecificOrigins", builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });

            });
        }
    }
}
