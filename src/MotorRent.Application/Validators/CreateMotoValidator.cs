using FluentValidation;
using MotorRent.Application.DTO;
using MotorRent.Application.Services.Interfaces;

namespace MotorRent.Application.Validators
{
    public class CreateMotoValidator : AbstractValidator<MotoDTO>
    {
        public CreateMotoValidator(IMotoService motoService)
        {
            RuleFor(x => x.identificador)
           .NotEmpty().WithMessage("Identificador é obrigatório.");

            RuleFor(x => x.ano)
                .GreaterThan(2000).WithMessage("O Ano precisa ser maior que 2000.");

            RuleFor(x => x.placa)
                .NotEmpty()
                .Matches(@"^([A-Z]{3}-\d{4}|[A-Z]{3}\d[A-Z]\d{2})$")
                .WithMessage("Formato de placa inváilo.")
                .MustAsync(motoService.PlateExistsAsync)
                .WithMessage("Placa já cadastrada.");

        }
    }
}
