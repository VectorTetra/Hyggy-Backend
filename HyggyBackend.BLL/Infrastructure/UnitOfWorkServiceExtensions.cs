using HyggyBackend.BLL.Interfaces;
using HyggyBackend.BLL.Services;
using HyggyBackend.BLL.Services.EmailService;
using HyggyBackend.DAL.EF;
using HyggyBackend.DAL.Interfaces;
using HyggyBackend.DAL.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace HyggyBackend.BLL.Infrastructure
{
    public static class UnitOfWorkServiceExtensions
    {
        public static void AddUnitOfWorkService(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITokenService, TokenService>();
			services.AddScoped<IShopService, ShopService>();
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<IMainStorageService, MainStorageService>();
			services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
		}
    }
}
