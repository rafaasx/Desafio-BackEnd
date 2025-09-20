using MotorRent.Application.Services.Interfaces;
using MotorRent.Domain.Entities;
using MotorRent.Domain.Interfaces;

namespace MotorRent.Application.Services
{
    public class MotoService(IMotoRepository repository) : ServiceBase<IMotoRepository, Moto>(repository), IMotoService
    {
        public Task<bool> PlateExistsAsync(string plate, CancellationToken cancellationToken)
        {
            return Repository.AnyAsync(a => a.Plate == plate, cancellationToken);
        }
    }
}
