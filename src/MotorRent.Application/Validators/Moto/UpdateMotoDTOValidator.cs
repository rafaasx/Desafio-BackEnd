using FluentValidation;
using MotorRent.Application.DTO.Moto;
using MotorRent.Application.Services.Interfaces;
using MotorRent.Domain.Constants;

namespace MotorRent.Application.Validators.Moto
{
    public class UpdateMotoDTOValidator : AbstractValidator<UpdateMotoDTO>
    {
        public UpdateMotoDTOValidator(IMotoService motoService)
        {
            RuleFor(x => x.placa)
                .NotEmpty()
                .Matches(@"^([A-Z]{3}-\d{4}|[A-Z]{3}\d[A-Z]\d{2})$")
                .WithMessage("Formato de placa inválido.")
                .MustAsync(async (plate, ct) => !await motoService.PlateExistsAsync(plate, ct))
                .WithMessage(Messages.PlateInUse);
        }
    }
}
