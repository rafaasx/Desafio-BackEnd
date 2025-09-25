using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MotorRent.Domain.Entities;

namespace MotorRent.Infrastructure.EntityConfiguration
{
    public class MotoConfiguration : IEntityTypeConfiguration<Moto>
    {
        public void Configure(EntityTypeBuilder<Moto> builder)
        {
            builder
            .ToTable("Motos");

            builder
                .HasKey(p => p.Identificador);
            builder.HasMany(p => p.Locacoes)
                .WithOne(p => p.Moto)
                .HasForeignKey(p => p.MotoId);

            builder.HasIndex(a => a.Identificador);

        }
    }
}
