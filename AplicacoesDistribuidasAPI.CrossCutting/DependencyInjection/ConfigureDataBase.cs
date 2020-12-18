using AplicacoesDistribuidasAPI.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace AplicacoesDistribuidasAPI.CrossCutting.DependencyInjection
{
    public class ConfigureDataBase
    {
        public static void ConfigureDependenciesDataBase(IServiceCollection serviceCollection, string connectionString)
        {

            serviceCollection.AddDbContext<DataBaseContext>(options =>
            {
                options.UseMySql(connectionString);
            });
        }
    }
}
