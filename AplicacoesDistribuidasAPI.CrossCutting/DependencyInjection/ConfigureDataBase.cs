using AplicacoesDistribuidasAPI.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace AplicacoesDistribuidasAPI.CrossCutting.DependencyInjection
{
    public class ConfigureDataBase
    {
        public static void ConfigureDependenciesDataBase(IServiceCollection serviceCollection, string connectionString)
        {

            serviceCollection.AddDbContext<DataBaseContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            
        }
    }
}
