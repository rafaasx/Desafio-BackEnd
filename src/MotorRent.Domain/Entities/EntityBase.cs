using MotorRent.Domain.Interfaces;

namespace MotorRent.Domain.Entities
{
    public class EntityBase : IEntityBase
    {
        public string Identificador { get; protected set; } = default!;
    }
}
