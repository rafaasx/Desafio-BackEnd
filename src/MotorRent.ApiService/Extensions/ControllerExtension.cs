using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MotorRent.Domain.Constants;

namespace MotorRent.ApiService.Extensions
{
    public static class ControllerExtensions
    {
        /// <summary>
        /// Valida o command usando o FluentValidation.
        /// Retorna BadRequest se inválido, ou o próprio objeto se válido.
        /// </summary>
        /// <typeparam name="T">Tipo do command</typeparam>
        /// <param name="controller">ControllerBase</param>
        /// <param name="dto">Objeto a ser validado</param>
        /// <returns>IActionResult se inválido, ou null se válido</returns>
        public static async Task<IActionResult?> ValidateDTOAsync<T>(
            this ControllerBase controller,
            T dto)
        {
            if (dto is null) return null;
            var validatorType = typeof(IValidator<>).MakeGenericType(dto.GetType());
            var validator = controller.HttpContext.RequestServices.GetService(validatorType);
            if (validator is IValidator baseValidator)
            {
                var result = await baseValidator.ValidateAsync(new ValidationContext<object>(dto));
                if (!result.IsValid)
                {
                    return controller.BadRequest(new
                    {
                        Message = Messages.InvalidData
                    });
                }
            }

            return null; // Válido, segue o fluxo
        }
    }
}
