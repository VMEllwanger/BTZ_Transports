using BTZ_Transports.Negocio.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BTZ_Transports.Dados.Mappings
{
    internal class RegistroAbastecimentoMapping : IEntityTypeConfiguration<RegistroAbastecimento>
    {
        public void Configure(EntityTypeBuilder<RegistroAbastecimento> builder)
        {
            builder.ToTable("RegistrosAbastecimento");
            builder.HasKey(k => k.Id);
            builder.Property(p => p.Data).HasColumnType("DATETIME").IsRequired();
            builder.Property(p => p.QuantidadeAbastecida).IsRequired();
            builder.Property(p => p.ValorTotal).IsRequired();
           
        }
    }
}
