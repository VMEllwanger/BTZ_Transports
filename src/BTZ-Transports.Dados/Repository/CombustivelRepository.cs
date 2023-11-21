using BTZ_Transports.Dados.Context;
using BTZ_Transports.Negocio.Interfaces;
using BTZ_Transports.Negocio.Models;
using Microsoft.EntityFrameworkCore;

namespace BTZ_Transports.Dados.Repository
{
    public class CombustivelRepository : ICombustivelRepository
    {
        protected readonly BTZContext Db;
        protected readonly DbSet<Combustivel> DbSet;

        public CombustivelRepository(BTZContext db)
        {
            Db = db;
            DbSet = db.Set<Combustivel>();
        }

        public async Task<Combustivel> ObterPorId(int id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task<List<Combustivel>> ObterTodos()
        {
            return await DbSet.ToListAsync();
        }

        public void Dispose()
        {
            Db?.Dispose();
        }
    }
}
