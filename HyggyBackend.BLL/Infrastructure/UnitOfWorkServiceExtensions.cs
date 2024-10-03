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
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IBlogService, BlogService>();
            services.AddScoped<IBlogCategory1Service, BlogCategory1Service>();
            services.AddScoped<IBlogCategory2Service, BlogCategory2Service>();
            services.AddScoped<IProffessionService, ProffessionService>();
            services.AddScoped<IEmployeeService<ShopEmployeeDTO>, ShopEmployeeDTOService>();
            services.AddScoped<ITokenService, TokenService>();
			services.AddScoped<IShopService, ShopService>();
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IEmailSender, EmailSender>();
            //services.AddScoped<IMainStorageService, MainStorageService>();
            services.AddScoped<IStorageService, StorageService>();
			services.AddScoped<IEmployeeService<StorageEmployeeDTO>, StorageEmployeeDTOService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IWareService, WareService>();
            //services.AddScoped<IWareCategoryService, WareCategoryService>();
            //services.AddScoped<IWareItemService, WareItemService>();
            services.AddScoped<IOrderItemService, OrderItemService>();
            services.AddScoped<IOrderStatusService, OrderStatusService>();
            services.AddScoped<IWareStatusService, WareStatusService>();
            services.AddScoped<IWarePriceHistoryService, WarePriceHistoryService>();
            services.AddScoped<IWareImageService, WareImageService>();
            services.AddScoped<IWareTrademarkService, WareTrademarkService>();
            services.AddScoped<IWareReviewService, WareReviewService>();
           



            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
		}
    }
}
