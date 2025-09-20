using Microsoft.Extensions.DependencyInjection;
using MotorRent.Application.Services;
using MotorRent.Application.Services.Interfaces;

namespace MotorRent.OutOfBox.Services
{
    public static class ServicesServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IDeliveryRiderService, DeliveryRiderService>();
            services.AddScoped<IMotoService, MotoService>();
            services.AddScoped<IRentalService, RentalService>();

            return services;
        }
    }
}
