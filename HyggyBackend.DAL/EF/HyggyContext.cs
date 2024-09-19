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
    public class HyggyContext : IdentityDbContext<User>
    {
        public HyggyContext(DbContextOptions<HyggyContext> options)
            : base(options)
        {
           
        }
        public DbSet<Address> Addresses { get; set; }
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
        public DbSet<MainStorage> MainStorages { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Address>().HasData(
                new Address { Id = 1, State = "Odessa", City = "Odessa", Street = "Shevchenko str.", HouseNumber = "23", PostalCode = "6600", Latitude = 48, Longitude = 38 }
                );

            builder.Entity<MainStorage>().HasData(
                new MainStorage { Id = 1, AddressId = 1 }
                );

            builder.Entity<OrderItem>()
                .HasOne(o => o.Ware)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(o => o.WareId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<OrderItem>()
                 .HasOne(o => o.Order)
                 .WithMany(o => o.OrderItems)
                 .HasForeignKey(o => o.OrderId)
                 .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<OrderItem>()
                .HasOne(o => o.PriceHistory)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(o => o.PriceHistoryId)
                .OnDelete(DeleteBehavior.NoAction);

			builder.Entity<Order>()
				.HasOne(o => o.Shop)
				.WithMany(o => o.Orders)
				.HasForeignKey(o => o.ShopId)
				.OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Shop>()
                .HasOne(s => s.Address)
                .WithOne(a => a.Shop)
                .OnDelete(DeleteBehavior.NoAction);

			builder.Entity<MainStorage>()
			   .HasOne(s => s.Address)
			   .WithOne(a => a.MainStorage)
			   .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<MainStorage>()
                .HasMany(m => m.Shops)
                .WithOne(s => s.Storage)
                .OnDelete(DeleteBehavior.SetNull);

			List <IdentityRole> roles = new List<IdentityRole>
			{
				new IdentityRole
				{
					Name = "Admin",
					NormalizedName = "ADMIN"
				},
				new IdentityRole
				{
					Name = "User",
					NormalizedName = "USER"
				},
			};
			builder.Entity<IdentityRole>().HasData(roles);
		}
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

}