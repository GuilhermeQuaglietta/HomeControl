using Microsoft.EntityFrameworkCore;

namespace HomeControl.Core.Infrastructure.EntityFramework
{
    public class BaseDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=.;database=myDb;trusted_connection=true;");
        }
    }
}
