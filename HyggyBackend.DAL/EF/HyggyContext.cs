using HyggyBackend.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using HyggyBackend.DAL.Entities.Employes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace HyggyBackend.DAL.EF
{
    public class HyggyContext : IdentityDbContext<IdentityUser>
    {
        public HyggyContext(DbContextOptions<HyggyContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
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

    public class SampleContextFactory : IDesignTimeDbContextFactory<HyggyContext>
    {
        public HyggyContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<HyggyContext>();

            // отримуємо конфігурацію з файлу appsettings.json
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            IConfigurationRoot config = builder.Build();

            // отримуємо рядок підключення з файлу appsettings.json
            string connectionString = config.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString)
                .UseLazyLoadingProxies();

            return new HyggyContext(optionsBuilder.Options);
        }
    }
}