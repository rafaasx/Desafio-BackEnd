using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MotorRent.Domain.Entities;

namespace MotorRent.Infrastructure.EntityConfiguration
{
    public class LocacaoConfiguration : IEntityTypeConfiguration<Locacao>
    {
        public void Configure(EntityTypeBuilder<Locacao> builder)
        {
            builder
            .ToTable("Locacoes");

            builder
                .HasKey(p => p.Identificador);
            builder.HasOne(p => p.Entregador)
                .WithMany()
                .HasForeignKey(p => p.EntregadorId);

            builder.HasOne(builder => builder.Moto)
                .WithMany(p => p.Locacoes)
                .HasForeignKey(p => p.MotoId);

            builder.HasIndex(a => a.Identificador);
            builder.HasIndex(a => a.MotoId);
            builder.HasIndex(a => a.EntregadorId);

        }
    }
}