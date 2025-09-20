using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using MotorRent.Application.DTO;
using MotorRent.Application.Validators;

namespace MotorRent.OutOfBox.Services
{
    public static class ValidatorsServiceCollectionExtensions
    {
        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<MotoDTO>, CreateMotoValidator>();
            return services;
        }
    }
}
