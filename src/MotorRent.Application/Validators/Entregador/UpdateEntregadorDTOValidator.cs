using FluentValidation;
using MotorRent.Application.DTO.Entregador;
using MotorRent.Application.Services.Interfaces;

namespace MotorRent.Application.Validators.Entregador
{
    public class UpdateEntregadorDTOValidator : AbstractValidator<UpdateEntregadorDTO>
    {
        public UpdateEntregadorDTOValidator(IMotoService motoService, ILocacaoService locacaoService)
        {
            RuleFor(x => x.imagem_cnh)
                .NotEmpty().WithMessage("Imagem da CNH é obrigatória.")
                .Must(BeValidBase64Image).WithMessage("A imagem da CNH deve ser um base64 válido nos formatos .png ou .bmp.");
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
