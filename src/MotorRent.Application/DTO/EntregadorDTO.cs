using MotorRent.Domain.Enums;

namespace MotorRent.Application.DTO
{
    public record EntregadorDTO(
        string identificador,
        string nome,
        string cnpj,
        DateTime data_nascimento,
        string numero_cnh,
        CnhTypeEnum tipo_cnh,
        string imagem_cnh);
}
