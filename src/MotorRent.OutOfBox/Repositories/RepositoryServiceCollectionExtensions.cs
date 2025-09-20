using Microsoft.Extensions.DependencyInjection;
using MotorRent.Domain.Interfaces;
using MotorRent.Infrastructure.Repositories;

namespace MotorRent.OutOfBox.Repositories
{
    public static class RepositoryServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IDeliveryRiderRepository, DeliveryRiderRepository>();
            services.AddScoped<IMotoRepository, MotoRepository>();
            services.AddScoped<IRentalRepository, RentalRepository>();

            return services;
        }
    }
}
