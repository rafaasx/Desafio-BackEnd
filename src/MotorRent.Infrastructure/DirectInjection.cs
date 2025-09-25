using Microsoft.Extensions.DependencyInjection;
using MotorRent.Application.DTO.Entregador;
using MotorRent.Application.DTO.Moto;
using MotorRent.Application.Services.Interfaces;
using MotorRent.Domain.Interfaces;
using MotorRent.Infrastructure.Messaging.Consumers;
using MotorRent.Infrastructure.Messaging.Services;
using MotorRent.Infrastructure.Repositories;
using Rebus.Handlers;

namespace MotorRent.Infrastructure
{
    public static class DirectInjection
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IEntregadorRepository, EntregadorRepository>();
            services.AddScoped<IMotoRepository, MotoRepository>();
            services.AddScoped<ILocacaoRepository, LocacaoRepository>();

            return services;
        }

        public static IServiceCollection AddInfraServices(this IServiceCollection services)
        {
            services.AddScoped<INotificationService, NotificationService>();

            return services;
        }

        public static IServiceCollection AddConsumers(this IServiceCollection services)
        {
            services.AddScoped<IHandleMessages<CreateMotoDTO>, CreateMotoConsumer>();
            services.AddScoped<IHandleMessages<CreateEntregadorDTO>, CreateEntregadorConsumer>();
            services.AddScoped<IHandleMessages<Created2024MotoDTO>, Created2024MotoConsumer>();

            return services;
        }
    }
}
