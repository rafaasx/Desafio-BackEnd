using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using MotorRent.Application.DTO.Entregador;
using MotorRent.Application.DTO.Locacao;
using MotorRent.Application.DTO.Moto;
using MotorRent.Application.Services;
using MotorRent.Application.Services.Interfaces;
using MotorRent.Application.Validators.Entregador;
using MotorRent.Application.Validators.Locacao;
using MotorRent.Application.Validators.Moto;
using MotorRent.Application.Validators.Moto.MotorRent.Application.Validators;

namespace MotorRent.Application
{
    public static class DirectInjection
    {
        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<CreateMotoDTO>, CreateMotoDTOValidator>();
            services.AddScoped<IValidator<UpdateMotoDTO>, UpdateMotoDTOValidator>();
            services.AddScoped<IValidator<DeleteMotoDTO>, DeleteMotoDTOValidator>();
            services.AddScoped<IValidator<CreateLocacaoDTO>, CreateLocacaoDTOValidator>();
            services.AddScoped<IValidator<CreateEntregadorDTO>, CreateEntregadorDTOValidator>();
            services.AddScoped<IValidator<UpdateEntregadorDTO>, UpdateEntregadorDTOValidator>();

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IEntregadorService, EntregadorService>();
            services.AddScoped<IMotoService, MotoService>();
            services.AddScoped<ILocacaoService, LocacaoService>();

            return services;
        }
    }
}
