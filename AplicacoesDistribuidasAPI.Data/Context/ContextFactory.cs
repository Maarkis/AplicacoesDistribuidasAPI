using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace AplicacoesDistribuidasAPI.Data.Context
{
    public class ContextFactory : IDesignTimeDbContextFactory<DataBaseContext>
    {

        public DataBaseContext CreateDbContext(string[] args)
        {

            string connectString = "Server=localhost\\SQLEXPRESS;Database=aplicacoesDistribuidas;Trusted_Connection=True;";
            //string connectString = "server=localhost;port=3306;database=aplicacoesDistribuidas;uid=root;password=123456";
            DbContextOptionsBuilder<DataBaseContext> optionBuilder = new DbContextOptionsBuilder<DataBaseContext>();
            optionBuilder.UseSqlServer(connectString);
            
            return new DataBaseContext(optionBuilder.Options);
        }
    }
}
