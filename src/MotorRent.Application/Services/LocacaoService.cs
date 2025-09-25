using MotorRent.Application.DTO.Locacao;
using MotorRent.Application.Services.Interfaces;
using MotorRent.Application.ViewModels;
using MotorRent.Domain.Constants;
using MotorRent.Domain.Entities;
using MotorRent.Domain.Interfaces;

namespace MotorRent.Application.Services
{
    public class LocacaoService(ILocacaoRepository repository) : ServiceBase<ILocacaoRepository, Locacao>(repository), ILocacaoService
    {
        public async Task UpdateReturnedAtAsync(string id, UpdateLocacaoDTO request)
        {
            var locacao = await Repository.GetByIdAsync(id);
            if (locacao is null)
                throw new KeyNotFoundException(Messages.RentalNotFound);
            locacao.UpdateReturnedAt(request.data_devolucao);
            await Repository.SaveChangesAsync();
        }

        public async Task<bool> LocacaoExistsAsync(string motoId, CancellationToken cancellationToken)
        {
            return await Repository.AnyAsync(a => a.MotoId == motoId, cancellationToken);
        }

        public async Task CreateAsync(CreateLocacaoDTO locacaoDto)
        {
            var locacao = new Locacao(locacaoDto.entregador_id, locacaoDto.moto_id, locacaoDto.plano, locacaoDto.data_inicio, locacaoDto.data_previsao_termino);
            await Repository.AddAsync(locacao);
            await Repository.SaveChangesAsync();
        }

        public async Task<LocacaoViewModel?> GetViewModelByIdAsync(string id)
        {
            var locacao = await GetByIdAsync(id);
            if (locacao is null) return null;
            return new LocacaoViewModel(
                locacao.Identificador,
                locacao.ValorDiaria,
                locacao.EntregadorId,
                locacao.MotoId,
                locacao.DataInicio,
                locacao.DataTermino,
                locacao.DataPrevisaoTermino,
                locacao.DataTermino);

        }
    }
}
