using BTZ_Transports.Negocio.Models;
using Microsoft.EntityFrameworkCore;

namespace BTZ_Transports.Dados.Context
{
    public class BTZContext : DbContext
    {
        public BTZContext(DbContextOptions<BTZContext> options) : base(options)
        {
        }
        public DbSet<Veiculo> Veiculos { get; set; }
        public DbSet<Motorista> Motoristas { get; set; }
        public DbSet<RegistroAbastecimento> RegistrosAbastecimento { get; set; }
        public DbSet<Combustivel> Combustiveis { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BTZContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
