using HyggyBackend.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace HyggyBackend.DAL.EF
{
    public class HyggyContext : DbContext
    {
        public HyggyContext(DbContextOptions<HyggyContext> _options)
           : base(_options)
        {
        }

        public DbSet<Ware> Wares { get; set; }

    }
    // Класс необходим исключительно для миграций
    public class SampleContextFactory : IDesignTimeDbContextFactory<HyggyContext>
    {
        public HyggyContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<HyggyContext>();

            // получаем конфигурацию из файла appsettings.json
            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            IConfigurationRoot config = builder.Build();

            // получаем строку подключения из файла appsettings.json
            string connectionString = config.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString).UseLazyLoadingProxies();
            return new HyggyContext(optionsBuilder.Options);
        }
    }
}
