using Microsoft.Extensions.Logging;
using MotorRent.Application.DTO.Moto;
using MotorRent.Application.Services.Interfaces;
using Rebus.Handlers;
using System.Diagnostics;

namespace MotorRent.Infrastructure.Messaging.Consumers
{
    public class CreateMotoConsumer(ILogger<CreateMotoConsumer> logger, IMotoService motoService) : IHandleMessages<CreateMotoDTO>
    {
        public async Task Handle(CreateMotoDTO message)
        {
            logger.LogInformation("Processando requisição {TraceId}", Activity.Current?.TraceId);
            try
            {
                await motoService.CreateAsync(message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Erro ao processar a moto {message.identificador}");
                throw;
            }
            logger.LogInformation("Finalizado requisição {TraceId}", Activity.Current?.TraceId);
        }
    }
}
