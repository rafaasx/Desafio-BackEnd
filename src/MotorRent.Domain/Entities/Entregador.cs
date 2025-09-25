using MotorRent.Domain.Enums;

namespace MotorRent.Domain.Entities
{
    public class Entregador : EntityBase
    {
        protected Entregador()
        {
        }

        public Entregador(string id, string nome, string cnpj, DateTime dataNascimento, string numeroCnh, TipoCnhEnum tipoCnh, string? imagemCnhPath)
        {
            Identificador = id;
            Nome = nome;
            Cnpj = cnpj;
            DataNascimento = dataNascimento;
            NumeroCnh = numeroCnh;
            TipoCnh = tipoCnh;
            ImagemCnhPath = imagemCnhPath;
        }

        public string Nome { get; private set; } = null!;
        public string Cnpj { get; private set; } = null!;
        public DateTime DataNascimento { get; private set; }
        public string NumeroCnh { get; private set; } = null!;
        public TipoCnhEnum TipoCnh { get; private set; }
        public string? ImagemCnhPath { get; private set; }
        public void UpdateImagemCnh(string cnhImageUrl) => ImagemCnhPath = cnhImageUrl;
    }
}
