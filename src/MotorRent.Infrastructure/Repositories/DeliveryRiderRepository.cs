using MotorRent.Domain.Entities;
using MotorRent.Domain.Interfaces;

namespace MotorRent.Infrastructure.Repositories
{
    public class DeliveryRiderRepository(ApplicationDbContext context) : Repository<DeliveryRider>(context), IDeliveryRiderRepository
    {
    }
}
