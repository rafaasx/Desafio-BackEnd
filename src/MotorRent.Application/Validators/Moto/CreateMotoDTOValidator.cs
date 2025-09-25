using FluentValidation;
using MotorRent.Application.DTO.Moto;
using MotorRent.Application.Services.Interfaces;
using MotorRent.Domain.Constants;

namespace MotorRent.Application.Validators.Moto
{
    public class CreateMotoDTOValidator : AbstractValidator<CreateMotoDTO>
    {
        public CreateMotoDTOValidator(IMotoService motoService)
        {
            RuleFor(x => x.identificador)
                .NotEmpty().WithMessage("Identificador é obrigatório.")
                .MustAsync(async (identificador, token) => !await motoService.IdExistsAsync(identificador, token))
                .WithMessage("Identificador já existe.");

            RuleFor(x => x.ano)
                .GreaterThan(2000).WithMessage("O Ano precisa ser maior que 2000.");

            RuleFor(x => x.modelo)
                .NotEmpty().WithMessage("O modelo é obrigatório.");

            RuleFor(x => x.placa)
                .NotEmpty()
                .Matches(@"^([A-Z]{3}-\d{4}|[A-Z]{3}\d[A-Z]\d{2})$")
                .WithMessage("Formato de placa inválido.")
                .MustAsync(async (plate, ct) => !await motoService.PlateExistsAsync(plate, ct))
                .WithMessage(Messages.PlateInUse);
        }
    }
}
