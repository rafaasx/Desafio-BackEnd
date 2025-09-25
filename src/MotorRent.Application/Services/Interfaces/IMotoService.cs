using MotorRent.Application.DTO.Moto;
using MotorRent.Application.ViewModels;
using MotorRent.Domain.Entities;

namespace MotorRent.Application.Services.Interfaces
{
    public interface IMotoService : IServiceBase<Moto>
    {
        IQueryable<MotoViewModel> QueryByPlate(string? plate);
        Task<bool> PlateExistsAsync(string plate, CancellationToken token = default);
        Task UpdatePlateAsync(string id, UpdateMotoDTO updateMotoDto);
        Task<MotoViewModel?> GetViewModelByIdAsync(string id);
        Task CreateAsync(CreateMotoDTO createMotoDto);
    }
}
