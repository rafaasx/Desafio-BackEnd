using Microsoft.Extensions.Logging;
using MotorRent.Application.DTO.Locacao;
using MotorRent.Application.Services.Interfaces;
using Newtonsoft.Json;
using Rebus.Handlers;
using System.Diagnostics;

namespace MotorRent.Infrastructure.Messaging.Consumers
{
    public class CreateLocacaoConsumer(ILogger<CreateLocacaoConsumer> logger, ILocacaoService service) : IHandleMessages<CreateLocacaoDTO>
    {
        public async Task Handle(CreateLocacaoDTO message)
        {
            logger.LogInformation("Processando requisição {TraceId}", Activity.Current?.TraceId);
            try
            {
                await service.CreateAsync(message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Erro ao processar a locação {JsonConvert.SerializeObject(message)}");
                throw;
            }
            logger.LogInformation("Finalizado requisição {TraceId}", Activity.Current?.TraceId);

        }
    }
}
