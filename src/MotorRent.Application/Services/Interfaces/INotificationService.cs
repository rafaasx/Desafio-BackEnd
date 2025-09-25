using MotorRent.Domain.Entities;

namespace MotorRent.Application.Services.Interfaces
{
    public interface INotificationService
    {
        Task NotifyMoto2024RegisteredAsync(Moto moto);
    }
}
