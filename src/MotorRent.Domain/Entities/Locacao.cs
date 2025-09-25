using MotorRent.Domain.Enums;

namespace MotorRent.Domain.Entities
{
    public class Locacao : EntityBase
    {
        protected Locacao()
        {
        }

        public Locacao(string entregadorId, string motoId, TipoPlanoEnum plano, DateTime dataInicio, DateTime dataPrevisaoTermino)
        {
            Identificador = Guid.NewGuid().ToString();
            EntregadorId = entregadorId;
            MotoId = motoId;
            Plano = plano;
            DataInicio = dataInicio;
            DataPrevisaoTermino = dataPrevisaoTermino;
            ValorDiaria = CalculateValorDiaria(plano);
        }

        public string EntregadorId { get; private set; }
        public Entregador Entregador { get; private set; } = null!;
        public string MotoId { get; private set; }
        public Moto Moto { get; private set; } = null!;
        public TipoPlanoEnum Plano { get; private set; }
        public decimal ValorDiaria { get; private set; }
        public DateTime DataInicio { get; private set; }
        public DateTime DataPrevisaoTermino { get; private set; }
        public DateTime? DataTermino { get; private set; }
        public decimal? TotalPago { get; private set; }

        public void UpdateReturnedAt(DateTime dataTermino)
        {
            DataTermino = dataTermino;
            TotalPago = CalculateTotalPago();
        }

        private decimal CalculateValorDiaria(TipoPlanoEnum plano) =>
            plano switch
            {
                TipoPlanoEnum.Plan7 => 30m,
                TipoPlanoEnum.Plan15 => 28m,
                TipoPlanoEnum.Plan30 => 22m,
                TipoPlanoEnum.Plan45 => 20m,
                TipoPlanoEnum.Plan50 => 18m,
                _ => throw new ArgumentOutOfRangeException(nameof(plano), $"Plano inválido: {plano}")
            };

        public decimal CalculateTotalPago()
        {
            if (!DataTermino.HasValue)
                throw new InvalidOperationException("Data de término não foi informada.");

            var diasContratados = (DataPrevisaoTermino - DataInicio).Days;
            var diasEfetivos = (DataTermino.Value - DataInicio).Days;

            if (IsDevolucaoAntesDoPrazo())
                return CalculateTotalPagoAntesDoPrazo(diasContratados, diasEfetivos);

            if (IsDevolucaoDepoisDoPrazo())
                return CalculateTotalPagoForaDoPrazo(diasContratados);

            return diasContratados * ValorDiaria;
        }

        private decimal CalculateTotalPagoForaDoPrazo(int diasContratados)
        {
            var valorPlano = diasContratados * ValorDiaria;
            var diasAdicionais = (DataTermino!.Value - DataPrevisaoTermino).Days;
            var adicional = diasAdicionais * 50m;

            return valorPlano + adicional;
        }

        private decimal CalculateTotalPagoAntesDoPrazo(int diasContratados, int diasEfetivos)
        {
            var diasUsados = diasEfetivos;
            var valorUsado = diasUsados * ValorDiaria;

            var diasNaoUsados = diasContratados - diasUsados;
            var valorNaoUsado = diasNaoUsados * ValorDiaria;

            var percentualMulta = Plano switch
            {
                TipoPlanoEnum.Plan7 => 0.20m,
                TipoPlanoEnum.Plan15 => 0.40m,
                _ => 0m
            };

            var multa = valorNaoUsado * percentualMulta;
            return valorUsado + multa;
        }

        private bool IsDevolucaoAntesDoPrazo() => DataTermino.HasValue && DataTermino.Value < DataPrevisaoTermino;
        private bool IsDevolucaoDepoisDoPrazo() => DataTermino.HasValue && DataTermino.Value > DataPrevisaoTermino;
    }
}
