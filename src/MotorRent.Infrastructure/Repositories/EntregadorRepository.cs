using MotorRent.Domain.Entities;
using MotorRent.Domain.Interfaces;

namespace MotorRent.Infrastructure.Repositories
{
    public class EntregadorRepository(ApplicationDbContext context) : Repository<Entregador>(context), IEntregadorRepository
    {
    }
}
