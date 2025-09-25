using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MotorRent.Domain.Entities;

namespace MotorRent.Infrastructure.EntityConfiguration
{
    public class EntregadorConfiguration : IEntityTypeConfiguration<Entregador>
    {
        public void Configure(EntityTypeBuilder<Entregador> builder)
        {
            builder
            .ToTable("Entregadores");

            builder
                .HasKey(p => p.Identificador);

            builder.HasIndex(a => a.Identificador);

        }
    }
}
