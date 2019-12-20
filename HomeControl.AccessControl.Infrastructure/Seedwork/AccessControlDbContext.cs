using HomeControl.AccessControl.Domain.Users;
using HomeControl.AccessControl.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;
using System;

namespace HomeControl.AccessControl.Infrastructure.Seedwork
{
    public class AccessControlDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public AccessControlDbContext(DbContextOptions options)
            : base(options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}
