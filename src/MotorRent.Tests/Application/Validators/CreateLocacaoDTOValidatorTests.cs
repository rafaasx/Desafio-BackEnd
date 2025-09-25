using MotorRent.Application.DTO.Locacao;
using MotorRent.Application.Services.Interfaces;
using MotorRent.Application.Validators.Locacao;
using MotorRent.Domain.Entities;
using MotorRent.Domain.Enums;

namespace MotorRent.Tests.Application.Validators
{
    using FluentValidation.TestHelper;
    using Moq;
    using System;
    using System.Threading.Tasks;
    using Xunit;

    namespace MotorRent.Tests.Validators
    {
        public class CreateLocacaoDTOValidatorTests
        {
            private readonly Mock<IMotoService> _motoServiceMock;
            private readonly Mock<IEntregadorService> _entregadorServiceMock;
            private readonly CreateLocacaoDTOValidator _validator;

            public CreateLocacaoDTOValidatorTests()
            {
                _motoServiceMock = new Mock<IMotoService>();
                _entregadorServiceMock = new Mock<IEntregadorService>();
                _validator = new CreateLocacaoDTOValidator(_motoServiceMock.Object, _entregadorServiceMock.Object);
            }

            private CreateLocacaoDTO CriarDTOValido() =>
                new CreateLocacaoDTO(
                    entregador_id: "entregador1",
                    moto_id: "moto1",
                    data_inicio: DateTime.UtcNow.Date.AddDays(1),
                    data_termino: DateTime.UtcNow.Date.AddDays(7),
                    data_previsao_termino: DateTime.UtcNow.Date.AddDays(7),
                    plano: TipoPlanoEnum.Plan7
                );

            [Fact]
            public async Task Deve_Passar_Quando_DTO_Valido()
            {
                // Arrange
                var dto = CriarDTOValido();
                _motoServiceMock.Setup(x => x.GetByIdAsync(dto.moto_id))
                    .ReturnsAsync(new Moto("moto1", "ModeloX", 2023, "ABC1234"));
                _entregadorServiceMock.Setup(x => x.GetByIdAsync(dto.entregador_id))
                    .ReturnsAsync(new Entregador("entregador1", "João", "12345678901234",
                        DateTime.Today.AddYears(-30), "12345678900", TipoCnhEnum.A, "img.png"));

                // Act
                var result = await _validator.TestValidateAsync(dto);

                // Assert
                result.ShouldNotHaveAnyValidationErrors();
            }

            [Theory]
            [InlineData(TipoCnhEnum.A, false)]
            [InlineData(TipoCnhEnum.AB, false)]
            [InlineData(TipoCnhEnum.B, true)]
            public async Task Deve_Falhar_Quando_Entregador_Tem_Cnh_Invalida(TipoCnhEnum tipoCnh, bool error)
            {
                var dto = CriarDTOValido();
                _entregadorServiceMock.Setup(x => x.GetByIdAsync(dto.entregador_id))
                    .ReturnsAsync(new Entregador("entregador1", "João", "12345678901234",
                        DateTime.Today.AddYears(-25), "12345678900", tipoCnh, null));
                _motoServiceMock.Setup(x => x.GetByIdAsync(dto.moto_id))
                    .ReturnsAsync(new Moto("moto1", "ModeloX", 2023, "ABC1234"));

                var result = await _validator.TestValidateAsync(dto);

                if (error)
                    result.ShouldHaveValidationErrorFor(x => x.entregador_id)
                        .WithErrorMessage("Entregador precisa de CNH A ou AB.");
                else
                    result.ShouldNotHaveValidationErrorFor(x => x.entregador_id);
            }

            [Fact]
            public async Task Deve_Falhar_Quando_EntregadorNaoExiste()
            {
                var dto = CriarDTOValido();
                _entregadorServiceMock.Setup(x => x.GetByIdAsync(dto.entregador_id))
                    .ReturnsAsync((Entregador?)null);

                var result = await _validator.TestValidateAsync(dto);

                result.ShouldHaveValidationErrorFor(x => x.entregador_id)
                    .WithErrorMessage("Entregador precisa de CNH A ou AB.");
            }

            [Fact]
            public async Task Deve_Falhar_Quando_MotoNaoExiste()
            {
                var dto = CriarDTOValido();
                _motoServiceMock.Setup(x => x.GetByIdAsync(dto.moto_id))
                    .ReturnsAsync((Moto?)null);

                var result = await _validator.TestValidateAsync(dto);

                result.ShouldHaveValidationErrorFor(x => x.moto_id)
                    .WithErrorMessage("Moto não cadastrada.");
            }

            [Fact]
            public async Task Deve_Falhar_Quando_PlanoInvalido()
            {
                var dto = CriarDTOValido() with { plano = (TipoPlanoEnum)999 };

                var result = await _validator.TestValidateAsync(dto);

                result.ShouldHaveValidationErrorFor(x => x.plano)
                    .WithErrorMessage("Plano é inválido.");
            }

            [Theory]
            [InlineData(0, 7, 7, "Data de início precisa ser o primeiro dia após o cadastro.")]
            [InlineData(1, 0, 7, "Data de previsão do término precisa ser maior que a data de início.")]
            [InlineData(1, 7, 0, "Data do término precisa ser maior ou igual a data de início.")]
            public async Task Deve_Falhar_Quando_DatasInvalidas(
                int diasInicio, int diasPrevisao, int diasTermino, string mensagemEsperada)
            {
                var dto = new CreateLocacaoDTO(
                    entregador_id: "entregador1",
                    moto_id: "moto1",
                    data_inicio: DateTime.UtcNow.Date.AddDays(diasInicio),
                    data_previsao_termino: DateTime.UtcNow.Date.AddDays(diasPrevisao),
                    data_termino: DateTime.UtcNow.Date.AddDays(diasTermino),
                    plano: TipoPlanoEnum.Plan7
                );

                var result = await _validator.TestValidateAsync(dto);

                result.ShouldHaveValidationErrors()
                    .WithErrorMessage(mensagemEsperada);
            }
        }
    }
}
