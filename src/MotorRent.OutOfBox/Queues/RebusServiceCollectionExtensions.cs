using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MotorRent.Application.DTO;
using Rebus.Config;
using System.Reflection;

namespace MotorRent.OutOfBox.Queues
{
    public static class RebusServiceCollectionExtensions
    {
        public static IServiceCollection AddBus(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var subscriptionBuilder = new SubscriptionBuilder()
            .Add<MotoDTO>();
            services.AutoRegisterHandlersFromAssembly(Assembly.GetEntryAssembly());
            services.AddScoped<IMessageSender, RebusMessageSender>();

            services.AddRebus(c => c
                .Transport(t =>
                {
                    t.UseRabbitMq(configuration.GetConnectionString("RabbitMq"), "MotoRent");
                }),
                onCreated: async bus =>
                {
                    if (subscriptionBuilder != null)
                    {
                        foreach (var type in subscriptionBuilder.TypesToSubscribe)
                        {
                            await bus.Subscribe(type);
                        }
                    }

                });

            return services;
        }
    }

    public class SubscriptionBuilder
    {
        internal List<Type> TypesToSubscribe { get; private set; } = [];

        public SubscriptionBuilder Add<T>()
        {
            TypesToSubscribe.Add(typeof(T));

            return this;
        }
    }
}
