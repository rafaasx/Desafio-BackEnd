using MotorRent.Domain.Entities;
using Xunit;

namespace MotorRent.Tests.Domain.Entities
{
    public class MotoTests
    {
        [Theory]
        [InlineData("moto1", "Honda CG", 2023, "ABC1234")]
        [InlineData("moto2", "Yamaha XTZ", 2024, "XYZ9876")]
        [InlineData("moto3", "Suzuki Burgman", 2025, "DEF5678")]
        public void Deve_Criar_Moto_Com_Propriedades_Corretas(string id, string modelo, int ano, string placa)
        {
            // Act
            var moto = new Moto(id, modelo, ano, placa);

            // Assert
            Assert.Equal(id, moto.Identificador);
            Assert.Equal(modelo, moto.Modelo);
            Assert.Equal(ano, moto.Ano);
            Assert.Equal(placa, moto.Placa);
            Assert.NotNull(moto.Locacoes);
        }

        [Theory]
        [InlineData("ABC1234", "XYZ9876")]
        [InlineData("DEF5678", "LMN4321")]
        public void Deve_Atualizar_Placa(string placaOriginal, string placaNova)
        {
            // Arrange
            var moto = new Moto("moto1", "Honda CG", 2023, placaOriginal);

            // Act
            moto.UpdatePlate(placaNova);

            // Assert
            Assert.Equal(placaNova, moto.Placa);
        }

        [Theory]
        [InlineData(2024, true)]
        [InlineData(2023, false)]
        [InlineData(2025, false)]
        public void IsYear2024_Deve_Retornar_Corretamente(int ano, bool esperado)
        {
            // Arrange
            var moto = new Moto("moto1", "Honda CG", ano, "ABC1234");

            // Act
            var result = moto.IsYear2024;

            // Assert
            Assert.Equal(esperado, result);
        }
    }
}
