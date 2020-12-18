using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace AplicacoesDistribuidasAPI.Data.Context
{
    public class ContextFactory : IDesignTimeDbContextFactory<DataBaseContext>
    {
        //private readonly IConfiguration _configuration;

        //public ContextFactory(IConfiguration configuration)
        //{
        //    _configuration = configuration;
        //}
        public DataBaseContext CreateDbContext(string[] args)
        {
            string connectString = "server=localhost;port=3306;database=aplicacoesDistribuidas;uid=root;password=123456";
            DbContextOptionsBuilder<DataBaseContext> optionBuilder = new DbContextOptionsBuilder<DataBaseContext>();
            optionBuilder.UseMySql(connectString);
            return new DataBaseContext(optionBuilder.Options);

        }
    }
}
