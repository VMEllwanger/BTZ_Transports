using BTZ_Transports.Negocio.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BTZ_Transports.Dados.Mappings
{
    internal class MotoristaMapping : IEntityTypeConfiguration<Motorista>
    {
        public void Configure(EntityTypeBuilder<Motorista> builder)
        {
            builder.ToTable("Motoristas");
            builder.HasKey(k => k.Id);
            builder.Property(p => p.Nome).HasColumnType("VARCHAR(512)").IsRequired();
            builder.Property(p => p.CPF).HasColumnType("VARCHAR(20)").IsRequired();
            builder.Property(p => p.NumeroCNH).HasColumnType("VARCHAR(20)").IsRequired();
            builder.Property(p => p.CategoriaCNH).HasColumnType("VARCHAR(3)").IsRequired();
            builder.Property(p => p.DataDeNascimento).HasColumnType("DATETIME").IsRequired();
            builder.Property(p => p.Status).HasConversion<string>().IsRequired();
           


            builder.HasMany(f => f.RegistroAbastecimentos)
            .WithOne(p => p.Motorista)
            .HasForeignKey(p => p.MotoristaId)
             .OnDelete(DeleteBehavior.ClientNoAction); ;
        }
    }
}
