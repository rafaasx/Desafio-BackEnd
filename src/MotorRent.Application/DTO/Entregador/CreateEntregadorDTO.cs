using MotorRent.Domain.Enums;

namespace MotorRent.Application.DTO.Entregador
{
    public record CreateEntregadorDTO(
        string identificador,
        string nome,
        string cnpj,
        DateTime data_nascimento,
        string numero_cnh,
        TipoCnhEnum tipo_cnh,
        string imagem_cnh);
}
