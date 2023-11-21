using BTZ_Transports.Negocio.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BTZ_Transports.Dados.Mappings
{
    internal class CombustivelMapping : IEntityTypeConfiguration<Combustivel>
    {
        public void Configure(EntityTypeBuilder<Combustivel> builder)
        {
            builder.ToTable("Combustiveis");
            builder.HasKey(k => k.Id);

            builder.Property(p => p.Preco).HasPrecision(18, 2);

            builder.Property(p => p.Nome).HasMaxLength(15).IsRequired();

            builder.HasMany(f => f.RegistroAbastecimentos)
              .WithOne(p => p.Combustivel)
              .HasForeignKey(p => p.CombustivelId)
              .OnDelete(DeleteBehavior.ClientNoAction); ;

            builder.HasData(
                new Combustivel { Id = 1, Nome = "Gasolina", Preco = 4.29M },
                new Combustivel { Id = 2, Nome = "Etanol", Preco = 2.99M },
                new Combustivel { Id = 3, Nome = "Diesel", Preco = 2.09M }
            );
        }
    }
}

