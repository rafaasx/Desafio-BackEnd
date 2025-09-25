using MotorRent.Domain.Entities;
using MotorRent.Domain.Enums;
using Xunit;

namespace MotorRent.Tests.Domain.Entities
{
    public class LocacaoTests
    {
        [Theory]
        [InlineData(TipoPlanoEnum.Plan7, 30)]
        [InlineData(TipoPlanoEnum.Plan15, 28)]
        [InlineData(TipoPlanoEnum.Plan30, 22)]
        [InlineData(TipoPlanoEnum.Plan45, 20)]
        [InlineData(TipoPlanoEnum.Plan50, 18)]
        public void Deve_Definir_Valor_Diaria_Corretamente(TipoPlanoEnum plano, decimal valorEsperado)
        {
            // Arrange
            var inicio = new DateTime(2025, 1, 1);
            var termino = inicio.AddDays(7);

            // Act
            var locacao = new Locacao("entregador1", "moto1", plano, inicio, termino);

            // Assert
            Assert.Equal(valorEsperado, locacao.ValorDiaria);
        }

        [Fact]
        public void Deve_Lancar_Excecao_Se_CalcularTotalPago_Sem_DataTermino()
        {
            var inicio = new DateTime(2025, 1, 1);
            var terminoPrevisto = inicio.AddDays(7);
            var locacao = new Locacao("entregador1", "moto1", TipoPlanoEnum.Plan7, inicio, terminoPrevisto);

            Assert.Throws<InvalidOperationException>(() => locacao.CalculateTotalPago());
        }

        [Theory]
        [InlineData(TipoPlanoEnum.Plan7, 7, 7, 210)]  // devolvido no prazo
        [InlineData(TipoPlanoEnum.Plan15, 15, 15, 420)]
        [InlineData(TipoPlanoEnum.Plan30, 30, 30, 660)]
        public void Deve_Calcular_TotalPago_Quando_Devolvido_No_Prazo(TipoPlanoEnum plano, int dias, int diasEfetivos, decimal totalEsperado)
        {
            var inicio = new DateTime(2025, 1, 1);
            var terminoPrevisto = inicio.AddDays(dias);
            var locacao = new Locacao("entregador1", "moto1", plano, inicio, terminoPrevisto);

            locacao.UpdateReturnedAt(terminoPrevisto);

            Assert.Equal(totalEsperado, locacao.TotalPago);
        }

        [Theory]
        [InlineData(TipoPlanoEnum.Plan7, 7, 5, 162)]   // devolvido antes do prazo (20% multa)
        [InlineData(TipoPlanoEnum.Plan15, 15, 10, 336)] // devolvido antes do prazo (40% multa)
        [InlineData(TipoPlanoEnum.Plan30, 30, 25, 550)] // devolvido antes do prazo (0% multa)
        public void Deve_Calcular_TotalPago_Quando_Devolvido_Antes_Do_Prazo(TipoPlanoEnum plano, int diasContratados, int diasUsados, decimal totalEsperado)
        {
            var inicio = new DateTime(2025, 1, 1);
            var terminoPrevisto = inicio.AddDays(diasContratados);
            var locacao = new Locacao("entregador1", "moto1", plano, inicio, terminoPrevisto);

            locacao.UpdateReturnedAt(inicio.AddDays(diasUsados));

            Assert.Equal(totalEsperado, locacao.TotalPago);
        }

        [Theory]
        [InlineData(TipoPlanoEnum.Plan7, 7, 2, 310)]   // 2 dias adicionais
        [InlineData(TipoPlanoEnum.Plan15, 15, 3, 570)] // 3 dias adicionais
        [InlineData(TipoPlanoEnum.Plan30, 30, 5, 910)] // 5 dias adicionais
        public void Deve_Calcular_TotalPago_Quando_Devolvido_Depois_Do_Prazo(TipoPlanoEnum plano, int diasContratados, int diasExtras, decimal totalEsperado)
        {
            var inicio = new DateTime(2025, 1, 1);
            var terminoPrevisto = inicio.AddDays(diasContratados);
            var locacao = new Locacao("entregador1", "moto1", plano, inicio, terminoPrevisto);

            locacao.UpdateReturnedAt(terminoPrevisto.AddDays(diasExtras));

            Assert.Equal(totalEsperado, locacao.TotalPago);
        }
    }
}
