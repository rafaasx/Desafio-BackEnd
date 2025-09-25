namespace MotorRent.Application.Validators.Moto
{
    using FluentValidation;
    using global::MotorRent.Application.DTO.Entregador;
    using global::MotorRent.Application.Services.Interfaces;
    using global::MotorRent.Domain.Enums;

    namespace MotorRent.Application.Validators
    {
        public class CreateEntregadorDTOValidator : AbstractValidator<CreateEntregadorDTO>
        {
            public CreateEntregadorDTOValidator(IEntregadorService entregadorService)
            {
                RuleFor(x => x.identificador)
                    .NotEmpty().WithMessage("Identificador é obrigatório.")
                    .MustAsync(async (identificador, ct) => !await entregadorService.IdExistsAsync(identificador, ct))
                    .WithMessage("Identificador já existe.");


                RuleFor(x => x.nome)
                    .NotEmpty().WithMessage("Nome é obrigatório.")
                    .MaximumLength(200).WithMessage("Nome não pode ultrapassar 200 caracteres.");

                RuleFor(x => x.cnpj)
                    .NotEmpty().WithMessage("CNPJ é obrigatório.")
                    .Matches(@"^\d{14}$").WithMessage("CNPJ deve conter 14 dígitos numéricos.")
                    .MustAsync(async (cnpj, ct) => !await entregadorService.CnpjExistsAsync(cnpj, ct))
                    .WithMessage("CNPJ já cadastrado.");

                RuleFor(x => x.data_nascimento)
                    .NotEmpty().WithMessage("Data de nascimento é obrigatória.")
                    .Must(HaveMinimumAge).WithMessage("O entregador deve ter no mínimo 18 anos.");

                RuleFor(x => x.numero_cnh)
                    .NotEmpty().WithMessage("Número da CNH é obrigatório.")
                    .MaximumLength(20).WithMessage("Número da CNH inválido.")
                    .MustAsync(async (cnh, ct) => !await entregadorService.CnhExistsAsync(cnh, ct))
                    .WithMessage("CNH já cadastrado.");

                RuleFor(x => x.tipo_cnh)
                    .IsInEnum().WithMessage("Tipo da CNH inválido. Valores permitidos: A, B ou AB.")
                    .Must(BeValidCnhType).WithMessage("Tipo da CNH inválido.");

                RuleFor(x => x.imagem_cnh)
                    .NotEmpty().WithMessage("Imagem da CNH é obrigatória.")
                    .Must(BeValidBase64Image).WithMessage("A imagem da CNH deve ser um base64 válido nos formatos .png ou .bmp.");
            }

            private bool HaveMinimumAge(DateTime birthDate)
            {
                var age = DateTime.Today.Year - birthDate.Year;
                if (birthDate.Date > DateTime.Today.AddYears(-age)) age--;
                return age >= 18;
            }

            private bool BeValidCnhType(TipoCnhEnum tipo)
            {
                return tipo == TipoCnhEnum.A ||
                       tipo == TipoCnhEnum.B ||
                       tipo == TipoCnhEnum.AB;
            }

            private bool BeValidBase64Image(string base64)
            {
                if (string.IsNullOrWhiteSpace(base64)) return false;

                try
                {
                    var bytes = Convert.FromBase64String(base64);

                    // PNG magic number: 89 50 4E 47
                    if (bytes.Length > 4 && bytes[0] == 0x89 && bytes[1] == 0x50 && bytes[2] == 0x4E && bytes[3] == 0x47)
                        return true;

                    // BMP magic number: 42 4D
                    if (bytes.Length > 2 && bytes[0] == 0x42 && bytes[1] == 0x4D)
                        return true;

                    return false;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
