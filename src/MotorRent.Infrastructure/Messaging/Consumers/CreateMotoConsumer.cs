using MotorRent.Application.DTO;
using MotorRent.Application.Services.Interfaces;
using MotorRent.Domain.Entities;
using Rebus.Handlers;

namespace MotorRent.Application.Workers
{
    public class CreateMotoConsumer(IMotoService motoService) : IHandleMessages<MotoDTO>
    {
        public async Task Handle(MotoDTO message)
        {
            await motoService.AddAsync(new Moto(message.identificador, message.modelo, message.ano, message.placa));
            await motoService.SaveChangesAsync();
        }
    }
}
