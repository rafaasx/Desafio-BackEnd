using MotorRent.Application.DTO.Locacao;
using MotorRent.Application.ViewModels;
using MotorRent.Domain.Entities;

namespace MotorRent.Application.Services.Interfaces
{
    public interface ILocacaoService : IServiceBase<Locacao>
    {
        Task CreateAsync(CreateLocacaoDTO message);
        Task<LocacaoViewModel?> GetViewModelByIdAsync(string id);
        Task<bool> LocacaoExistsAsync(string motoId, CancellationToken cancellationToken);
        Task UpdateReturnedAtAsync(string id, UpdateLocacaoDTO request);
    }
}
