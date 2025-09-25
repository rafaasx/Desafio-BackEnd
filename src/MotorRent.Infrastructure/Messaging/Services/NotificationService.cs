using MotorRent.Application.DTO.Moto;
using MotorRent.Application.Services.Interfaces;
using MotorRent.Domain.Entities;
using MotorRent.OutOfBox.Queues;

namespace MotorRent.Infrastructure.Messaging.Services
{
    public class NotificationService(IMessageSender messageSender) : INotificationService
    {
        public async Task NotifyMoto2024RegisteredAsync(Moto moto)
        {
            var message = new Created2024MotoDTO(moto.Identificador, moto.Ano, moto.Modelo, moto.Placa);
            await messageSender.Publish(message);
        }
    }
}
