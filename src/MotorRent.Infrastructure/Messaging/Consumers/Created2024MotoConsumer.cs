using Microsoft.Extensions.Logging;
using MotorRent.Application.DTO.Moto;
using Newtonsoft.Json;
using Rebus.Handlers;
using System.Diagnostics;

namespace MotorRent.Infrastructure.Messaging.Consumers
{
    public class Created2024MotoConsumer(ILogger<Created2024MotoConsumer> logger) : IHandleMessages<Created2024MotoDTO>
    {
        public async Task Handle(Created2024MotoDTO message)
        {
            logger.LogInformation("Processando requisição {TraceId}", Activity.Current?.TraceId);

            logger.LogInformation($"Processando mensagem CreatedMotoDTO no consumer Moto2024CreatedConsumer: {JsonConvert.SerializeObject(message)}");
        }
    }
}
