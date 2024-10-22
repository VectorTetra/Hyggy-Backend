using AutoMapper;
using HyggyBackend.BLL.DTO.EmployeesDTO;
using HyggyBackend.BLL.Interfaces;
using HyggyBackend.BLL.Services;
using HyggyBackend.BLL.Services.EmailService;
using HyggyBackend.BLL.Services.Employees;
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
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<IBlogCategory1Service, BlogCategory1Service>();
            services.AddScoped<IBlogCategory2Service, BlogCategory2Service>();
            services.AddScoped<IBlogService, BlogService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<IEmployeeService<ShopEmployeeDTO>, ShopEmployeeDTOService>();
            services.AddScoped<IEmployeeService<StorageEmployeeDTO>, StorageEmployeeDTOService>();
            services.AddScoped<IOrderItemService, OrderItemService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderStatusService, OrderStatusService>();
            services.AddScoped<IProffessionService, ProffessionService>();
            services.AddScoped<IShopService, ShopService>();
            services.AddScoped<IStorageService, StorageService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IWareCategory1Service, WareCategory1Service>();
            services.AddScoped<IWareCategory2Service, WareCategory2Service>();
            services.AddScoped<IWareCategory3Service, WareCategory3Service>();
            services.AddScoped<IWareImageService, WareImageService>();
            services.AddScoped<IWareItemService, WareItemService>();
            services.AddScoped<IWarePriceHistoryService, WarePriceHistoryService>();
            services.AddScoped<IWareReviewService, WareReviewService>();
            services.AddScoped<IWareService, WareService>();
            services.AddScoped<IWareStatusService, WareStatusService>();
            services.AddScoped<IWareTrademarkService, WareTrademarkService>();


            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
		}
    }
}
