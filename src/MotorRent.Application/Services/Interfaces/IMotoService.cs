using MotorRent.Domain.Entities;

namespace MotorRent.Application.Services.Interfaces
{
    public interface IMotoService : IServiceBase<Moto>
    {
        Task<bool> PlateExistsAsync(string plate, CancellationToken token);
    }
}
