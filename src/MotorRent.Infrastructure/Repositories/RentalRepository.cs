using MotorRent.Domain.Entities;
using MotorRent.Domain.Interfaces;

namespace MotorRent.Infrastructure.Repositories
{
    public class RentalRepository(ApplicationDbContext context) : Repository<Rental>(context), IRentalRepository
    {
    }
}
