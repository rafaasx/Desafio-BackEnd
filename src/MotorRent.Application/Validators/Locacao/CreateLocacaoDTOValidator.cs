using FluentValidation;
using MotorRent.Application.DTO.Locacao;
using MotorRent.Application.Services.Interfaces;
using MotorRent.Domain.Enums;

namespace MotorRent.Application.Validators.Locacao
{
    public class CreateLocacaoDTOValidator : AbstractValidator<CreateLocacaoDTO>
    {
        public CreateLocacaoDTOValidator(IMotoService motoService, IEntregadorService entregadorService)
        {
            RuleFor(x => x.entregador_id)
                .NotEmpty().WithMessage("Entregador é obrigatório.")
                .MustAsync(async (id, ct) =>
                {
                    var entregador = await entregadorService.GetByIdAsync(id);
                    return entregador != null && (entregador.TipoCnh == TipoCnhEnum.A || entregador.TipoCnh == TipoCnhEnum.AB);
                })
                .WithMessage("Entregador precisa de CNH A ou AB.");

            RuleFor(x => x.moto_id)
                .NotEmpty().WithMessage("Moto é obrigatória.")
                .MustAsync(async (id, ct) =>
                {
                    var moto = await motoService.GetByIdAsync(id);
                    return moto != null;
                })
                .WithMessage("Moto não cadastrada.");

            RuleFor(x => x.plano)
                .IsInEnum().WithMessage("Plano é inválido.");

            RuleFor(x => x.data_inicio)
                .NotEmpty().WithMessage("Data de início é obrigatória.")
                .Must(dataInicio => dataInicio.Date >= DateTime.UtcNow.Date.AddDays(1))
                .WithMessage("Data de início precisa ser o primeiro dia após o cadastro.");

            RuleFor(x => x.data_previsao_termino)
                .NotEmpty().WithMessage("Data de previsão do término é obrigatória.")
                .GreaterThan(x => x.data_inicio).WithMessage("Data de previsão do término precisa ser maior que a data de início.");

            RuleFor(x => x.data_termino)
                .NotEmpty().WithMessage("Data do tírmino é obrigatória.")
                .GreaterThanOrEqualTo(x => x.data_inicio).WithMessage("Data do término precisa ser maior ou igual a data de início.");
        }
    }
}
