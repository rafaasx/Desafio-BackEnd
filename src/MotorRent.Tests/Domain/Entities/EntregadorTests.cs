using MotorRent.Domain.Entities;
using MotorRent.Domain.Enums;
using Xunit;

namespace MotorRent.Tests.Domain.Entities
{
    public class EntregadorTests
    {
        [Theory]
        [InlineData("ent1", "João Silva", "12.345.678/0001-90", "2000-01-01", "123456789", TipoCnhEnum.A, null)]
        [InlineData("ent2", "Maria Souza", "98.765.432/0001-01", "1995-05-15", "987654321", TipoCnhEnum.B, "cnh_maria.png")]
        [InlineData("ent3", "Carlos Lima", "11.222.333/0001-44", "1980-10-10", "112233445", TipoCnhEnum.AB, null)]
        public void Deve_Criar_Entregador_Com_Propriedades_Corretas(
            string id,
            string nome,
            string cnpj,
            string dataNascimentoString,
            string numeroCnh,
            TipoCnhEnum tipoCnh,
            string? imagemCnhPath)
        {
            // Arrange
            var dataNascimento = DateTime.Parse(dataNascimentoString);

            // Act
            var entregador = new Entregador(id, nome, cnpj, dataNascimento, numeroCnh, tipoCnh, imagemCnhPath);

            // Assert
            Assert.Equal(id, entregador.Identificador);
            Assert.Equal(nome, entregador.Nome);
            Assert.Equal(cnpj, entregador.Cnpj);
            Assert.Equal(dataNascimento, entregador.DataNascimento);
            Assert.Equal(numeroCnh, entregador.NumeroCnh);
            Assert.Equal(tipoCnh, entregador.TipoCnh);
            Assert.Equal(imagemCnhPath, entregador.ImagemCnhPath);
        }

        [Theory]
        [InlineData(null, "nova_imagem.png")]
        [InlineData("cnh_antiga.png", "atualizada.png")]
        public void Deve_Atualizar_ImagemCnh(string? imagemAtual, string imagemNova)
        {
            // Arrange
            var entregador = new Entregador("ent1", "João Silva", "12.345.678/0001-90", new DateTime(2000, 1, 1), "123456789", TipoCnhEnum.A, imagemAtual);

            // Act
            entregador.UpdateImagemCnh(imagemNova);

            // Assert
            Assert.Equal(imagemNova, entregador.ImagemCnhPath);
        }
    }
}
