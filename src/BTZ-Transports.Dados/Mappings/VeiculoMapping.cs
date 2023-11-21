using BTZ_Transports.Negocio.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BTZ_Transports.Dados.Mappings
{
    public class VeiculoMapping : IEntityTypeConfiguration<Veiculo>
    {
        public void Configure(EntityTypeBuilder<Veiculo> builder)
        {
            builder.ToTable("Veiculos");
            builder.HasKey(k => k.Id);
            builder.Property(p => p.Nome).HasColumnType("VARCHAR(512)").IsRequired();
            builder.Property(p => p.Fabricante).HasColumnType("VARCHAR(100)").IsRequired();
            builder.Property(p => p.AnoDeFabricacao).IsRequired();
            builder.Property(p => p.CapacidadeMaximaDoTanque).IsRequired();
            builder.Property(p => p.Observacao).HasColumnType("VARCHAR(1028)").IsRequired(false);
         

            builder.HasMany(f => f.RegistroAbastecimentos)
           .WithOne(p => p.Veiculo)
           .HasForeignKey(p => p.VeiculoId)
           .OnDelete(DeleteBehavior.ClientNoAction);

            builder.HasOne(v => v.Combustivel)
            .WithMany(c => c.Veiculos)
            .HasForeignKey(v => v.CombustivelId)
            .OnDelete(DeleteBehavior.ClientNoAction);
        }
    }
}
