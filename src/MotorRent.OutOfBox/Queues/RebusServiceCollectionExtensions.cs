using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rebus.Config;
using System.Reflection;

namespace MotorRent.OutOfBox.Queues
{
    public static class RebusServiceCollectionExtensions
    {
        public static IServiceCollection AddBus(
            this IServiceCollection services,
            IConfiguration configuration,
            SubscriptionBuilder subscriptionBuilder)
        {
            services.AutoRegisterHandlersFromAssembly(Assembly.GetEntryAssembly());
            services.AddScoped<IMessageSender, RebusMessageSender>();


            services.AddRebus(c => c
                .Transport(t =>
                {
                    t.UseRabbitMq(configuration.GetConnectionString("RabbitMq"), "MotoRent.Queue");
                }),
                onCreated: async bus =>
                {
                    foreach (var type in subscriptionBuilder.TypesToSubscribe)
                        await bus.Subscribe(type);
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
