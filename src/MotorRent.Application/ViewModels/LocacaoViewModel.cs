namespace MotorRent.Application.ViewModels
{
    public record LocacaoViewModel
    (
        string identificador,
        decimal valor_diaria,
        string entregador_id,
        string moto_id,
        DateTime data_inicio,
        DateTime? data_termino,
        DateTime data_previsao_termino,
        DateTime? data_devolucao
    );
}
