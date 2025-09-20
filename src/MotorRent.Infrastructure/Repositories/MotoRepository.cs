using MotorRent.Domain.Entities;
using MotorRent.Domain.Interfaces;

namespace MotorRent.Infrastructure.Repositories
{
    public class MotoRepository(ApplicationDbContext context) : Repository<Moto>(context), IMotoRepository
    {
    }
}
