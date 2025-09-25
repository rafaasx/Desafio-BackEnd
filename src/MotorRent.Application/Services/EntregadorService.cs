using Microsoft.Extensions.Configuration;
using MotorRent.Application.DTO.Entregador;
using MotorRent.Application.Services.Interfaces;
using MotorRent.Domain.Entities;
using MotorRent.Domain.Interfaces;

namespace MotorRent.Application.Services
{
    public class EntregadorService(IEntregadorRepository repository, IConfiguration configuration) : ServiceBase<IEntregadorRepository, Entregador>(repository), IEntregadorService
    {
        private readonly string cnhImagesFolder = configuration.GetSection("Parameters").GetSection("ImagesFolder").Value!;
        public async Task UpdateCnhImageAsync(string id, string cnhImageBase64)
        {
            var entregador = await Repository.GetByIdAsync(id);
            if (entregador == null)
                throw new Exception("Entregador não encontrado.");
            var cnhPath = Path.Combine(cnhImagesFolder, id);
            entregador.UpdateImagemCnh(cnhPath);
            await StoreImage(cnhImageBase64, cnhPath);
            await Repository.SaveChangesAsync();
        }

        public async Task CreateAsync(CreateEntregadorDTO entregador)
        {
            var cnhPath = Path.Combine(cnhImagesFolder, entregador.identificador);
            await Repository.AddAsync(new Entregador(
                entregador.identificador,
                entregador.nome,
                entregador.cnpj,
                entregador.data_nascimento,
                entregador.numero_cnh,
                entregador.tipo_cnh,
                cnhPath));
            await StoreImage(entregador.imagem_cnh, cnhPath);
            await Repository.SaveChangesAsync();
        }

        private static async Task StoreImage(string imagemCnh, string cnhPath)
        {
            var directory = Path.GetDirectoryName(cnhPath);
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory!);
            if (File.Exists(cnhPath))
                File.Delete(cnhPath);
            await File.WriteAllBytesAsync(cnhPath, Convert.FromBase64String(imagemCnh));
        }

        public Task<bool> CnpjExistsAsync(string cnpj, CancellationToken ct)
        {
            return Repository.AnyAsync(a => a.Cnpj == cnpj, ct);
        }

        public Task<bool> CnhExistsAsync(string numeroCnh, CancellationToken ct)
        {
            return Repository.AnyAsync(a => a.NumeroCnh == numeroCnh, ct);
        }

        public override async Task DeleteAsync(string id)
        {
            await Repository.DeleteAsync(id);
            await SaveChangesAsync();
        }
    }
}
