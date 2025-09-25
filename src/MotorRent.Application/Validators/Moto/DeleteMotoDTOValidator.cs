using FluentValidation;
using MotorRent.Application.DTO.Moto;
using MotorRent.Application.Services.Interfaces;

namespace MotorRent.Application.Validators.Moto
{
    public class DeleteMotoDTOValidator : AbstractValidator<DeleteMotoDTO>
    {
        public DeleteMotoDTOValidator(IMotoService motoService, ILocacaoService locacaoService)
        {
            RuleFor(x => x.id)
                .NotEmpty().WithMessage("Identificador é obrigatório.")
                .MustAsync(motoService.IdExistsAsync)
                .WithMessage("Identificador não existe.")
                .MustAsync(async (id, ct) => !await locacaoService.LocacaoExistsAsync(id, ct))
                .WithMessage("Não permitido excluir uma moto que já teve locação.");
        }
    }
}
