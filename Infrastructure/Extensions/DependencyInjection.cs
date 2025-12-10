using Application.Abstractions;
using Infrastructure.Data;
using Infrastructure.Repository;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseNpgsql(configuration.GetConnectionString("DbConnection"));
            });

            services.AddScoped<IOrderRepository, OrderRepository>();

            services.AddScoped<ICacheService,CacheService>();

            services.AddStackExchangeRedisCache(options =>
            {
              options.Configuration=  configuration.GetConnectionString("Redis");

            });
            return services;
        }
    }
}
