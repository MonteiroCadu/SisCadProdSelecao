using Domain;
using Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class CategoriaPersist : GeralPersist, ICategoriaPersist
    {
        public CategoriaPersist(AppDbContext contexto) : base(contexto)
        {
        }

        public async Task<IList<Categoria>> GetAllAsync(int top)
        {
            IQueryable<Categoria> query = _contexto.Categorias
                 .Take(top)
                 .OrderByDescending(p => p.Id)
                 .AsNoTracking();

            return await query.ToListAsync();
        }

        public async Task<Categoria?> GetByIdAsync(int? id = 0)
        {
            IQueryable<Categoria> query = _contexto.Categorias
                .Where(c => c.Id == id)
                .Include(c => c.Produtos)
                .AsNoTracking();

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Categoria?> GetByNameAsync(string nome)
        {
            IQueryable<Categoria> query = _contexto.Categorias
                .Where(c => c.Nome.Equals(nome))
                .Include(c => c.Produtos)
                .AsNoTracking();

            return await query.FirstOrDefaultAsync();
        }

        public async Task<IList<Categoria>> SearchByNameAsync(string nome)
        {
            IQueryable<Categoria> query = _contexto.Categorias
                .Where(c => c.Nome.Contains(nome))
                .AsNoTracking();

            return await query.ToListAsync();
        }
    }
}
