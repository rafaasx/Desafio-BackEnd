using MotorRent.Domain.Enums;

namespace MotorRent.Domain.Entities
{
    public class DeliveryRider : EntityBase
    {
        public DeliveryRider(string identifier, string name, string cnpj, DateTime birthDate, string cnhNumber, CnhTypeEnum cnhType, string? cnhImageUrl) : base(identifier)
        {
            Name = name;
            Cnpj = cnpj;
            BirthDate = birthDate;
            CnhNumber = cnhNumber;
            CnhType = cnhType;
            CnhImageUrl = cnhImageUrl;
        }

        public string Name { get; private set; } = null!;
        public string Cnpj { get; private set; } = null!;
        public DateTime BirthDate { get; private set; }
        public string CnhNumber { get; private set; } = null!;
        public CnhTypeEnum CnhType { get; private set; }
        public string? CnhImageUrl { get; private set; }
    }
}
