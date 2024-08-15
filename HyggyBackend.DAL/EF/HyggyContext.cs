using HyggyBackend.DAL.Entities;
using HyggyBackend.DAL.Entities.Employes;
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

        public DbSet<Customer> Customers{ get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<Proffession> Proffessions { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<Storage> Storages { get; set; }
        public DbSet<Ware> Wares { get; set; }
        public DbSet<WareCategory1> WareCategories1 { get; set; }
        public DbSet<WareCategory2> WareCategories2 { get; set; }
        public DbSet<WareCategory3> WareCategories3 { get; set; }
        public DbSet<WareImage> WareImages { get; set; }
        public DbSet<WarePriceHistory> WarePriceHistories { get; set; }
        public DbSet<WareStatus> WareStatuses { get; set; }
        public DbSet<ShopEmployee> ShopEmployees { get; set; }
        public DbSet<StorageEmployee> StorageEmployees { get; set; }

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
