using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MotorRent.OutOfBox.Database
{
    public static class EntityFrameworkServiceCollectionExtensions
    {
        public static IServiceCollection AddPostgresDbContext<TContext>(this IServiceCollection services, IConfiguration configuration)
            where TContext : DbContext
        {
            services.AddDbContext<TContext>((options) =>
            {
                options.UseNpgsql(configuration.GetConnectionString("MotorRent"));
            });

            return services;
        }
    }
}
