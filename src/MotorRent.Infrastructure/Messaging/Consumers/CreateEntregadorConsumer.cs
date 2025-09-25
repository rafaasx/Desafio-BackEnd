using Microsoft.Extensions.Logging;
using MotorRent.Application.DTO.Entregador;
using MotorRent.Application.Services.Interfaces;
using Rebus.Handlers;
using System.Diagnostics;

namespace MotorRent.Infrastructure.Messaging.Consumers
{
    public class CreateEntregadorConsumer(ILogger<CreateEntregadorConsumer> logger, IEntregadorService service) : IHandleMessages<CreateEntregadorDTO>
    {
        public async Task Handle(CreateEntregadorDTO message)
        {
            logger.LogInformation("Processando requisição {TraceId}", Activity.Current?.TraceId);
            try
            {
                await service.CreateAsync(message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao processar entregador {@EntregadorId}", message.identificador);
                throw;
            }
            logger.LogInformation("Finalizado requisição {TraceId}", Activity.Current?.TraceId);

        }
    }
}
