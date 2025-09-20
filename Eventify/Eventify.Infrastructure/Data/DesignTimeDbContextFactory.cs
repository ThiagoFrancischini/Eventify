using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace Eventify.Infrastructure.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<EventifyDbContext>
    {
        public EventifyDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<EventifyDbContext>();

            var connectionString = "User Id=postgres.njohggzekkqgghywtuwp;Password=!Teste12345;Server=aws-1-sa-east-1.pooler.supabase.com;Port=5432;Database=postgres";

            optionsBuilder.UseNpgsql(connectionString);

            return new EventifyDbContext(optionsBuilder.Options);
        }
    }
}
