using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AplicacoesDistribuidasAPI.Data.Context
{
    public class ContextFactory : IDesignTimeDbContextFactory<Context>
    {
        public Context CreateDbContext(string[] args)
        {

            // Usado para criar as migrações
            var connectString = "";
            var optionsBuilder = new DbContextOptionsBuilder<Context>();
            optionsBuilder.UseMySql(connectString);
            return new Context(optionsBuilder.Options);
        }
    }
}
