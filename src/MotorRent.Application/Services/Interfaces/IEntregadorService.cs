using MotorRent.Application.DTO.Entregador;
using MotorRent.Domain.Entities;

namespace MotorRent.Application.Services.Interfaces
{
    public interface IEntregadorService : IServiceBase<Entregador>
    {
        Task<bool> CnhExistsAsync(string numeroCnh, CancellationToken ct);
        Task<bool> CnpjExistsAsync(string cnpj, CancellationToken token);
        Task CreateAsync(CreateEntregadorDTO entregador);
        Task UpdateCnhImageAsync(string id, string cnhImageUrl);
    }
}
