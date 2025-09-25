
namespace MotorRent.Domain.Entities
{
    public class Moto : EntityBase
    {
        protected Moto()
        {
        }

        public Moto(string id, string modelo, int ano, string placa)
        {
            Identificador = id;
            Modelo = modelo;
            Ano = ano;
            Placa = placa;
        }
        public string Modelo { get; private set; }
        public int Ano { get; private set; }
        public string Placa { get; private set; } = null!;
        public ICollection<Locacao>? Locacoes { get; private set; } = [];
        public bool IsYear2024 => Ano == Constants.Constants.Year2024;

        public void UpdatePlate(string placa) => Placa = placa;
    }
}
