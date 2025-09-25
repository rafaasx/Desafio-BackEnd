using MotorRent.Domain.Entities;
using MotorRent.Domain.Interfaces;

namespace MotorRent.Infrastructure.Repositories
{
    public class LocacaoRepository(ApplicationDbContext context) : Repository<Locacao>(context), ILocacaoRepository
    {
    }
}
