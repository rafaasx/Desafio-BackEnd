using MotorRent.Application.DTO.Moto;
using MotorRent.Application.Services.Interfaces;
using MotorRent.Application.ViewModels;
using MotorRent.Domain.Constants;
using MotorRent.Domain.Entities;
using MotorRent.Domain.Interfaces;

namespace MotorRent.Application.Services
{
    public class MotoService(IMotoRepository repository, INotificationService notificationService) : ServiceBase<IMotoRepository, Moto>(repository), IMotoService
    {
        public Task<bool> PlateExistsAsync(string plate, CancellationToken cancellationToken)
        {
            return Repository.AnyAsync(a => a.Placa == plate, cancellationToken);
        }

        public IQueryable<MotoViewModel> QueryByPlate(string? plate) =>
            Repository.Query()
            .Where(f => string.IsNullOrWhiteSpace(plate) || f.Placa.ToLower() == plate.ToLower())
            .Select(s => new MotoViewModel(s.Identificador, s.Ano, s.Modelo, s.Placa));

        public async Task UpdatePlateAsync(string id, UpdateMotoDTO updateMotoDto)
        {
            var moto = await Repository.GetByIdAsync(id);
            if (moto is null)
                throw new KeyNotFoundException(Messages.MotoNotFound);
            moto.UpdatePlate(updateMotoDto.placa);
            await Repository.SaveChangesAsync();
        }

        public override async Task DeleteAsync(string id)
        {
            await Repository.DeleteAsync(id);
            await SaveChangesAsync();
        }

        public async Task<MotoViewModel?> GetViewModelByIdAsync(string id)
        {
            var moto = await GetByIdAsync(id);
            if (moto is null) return null;
            return new MotoViewModel(moto.Identificador, moto.Ano, moto.Modelo, moto.Placa);

        }

        public async Task CreateAsync(CreateMotoDTO createMotoDto)
        {
            var moto = new Moto(createMotoDto.identificador, createMotoDto.modelo, createMotoDto.ano, createMotoDto.placa);

            await Repository.AddAsync(moto);
            await Repository.SaveChangesAsync();

            if (moto.IsYear2024)
                await notificationService.NotifyMoto2024RegisteredAsync(moto);
        }

    }
}
