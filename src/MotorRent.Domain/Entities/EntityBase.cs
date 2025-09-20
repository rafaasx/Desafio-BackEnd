using MotorRent.Domain.Interfaces;

namespace MotorRent.Domain.Entities
{
    public class EntityBase : IEntityBase
    {
        public EntityBase(string identifier)
        {
            Identifier = identifier;
        }

        public string Identifier { get; private set; } = default!;
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    }
}
