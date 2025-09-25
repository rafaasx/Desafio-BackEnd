using MotorRent.Domain.Enums;

namespace MotorRent.Application.DTO.Locacao
{
    public record CreateLocacaoDTO(
        string entregador_id,
        string moto_id,
        DateTime data_inicio,
        DateTime data_termino,
        DateTime data_previsao_termino,
        TipoPlanoEnum plano);
}
