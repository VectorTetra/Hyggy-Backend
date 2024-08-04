using HyggyBackend.DAL.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HyggyBackend.BLL.Infrastructure
{
    public static class HyggyContextServiceExtensions
    {
        public static void AddHyggyContext(this IServiceCollection services, string connection)
        {
            services.AddDbContext<HyggyContext>(options => options.UseLazyLoadingProxies().UseSqlServer(connection));
        }
    }
}
