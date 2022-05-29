using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence.Interfaces;


namespace Persistence
{
    public class ProdutoPersist : GeralPersist, IProdutoPersist
    {
        public ProdutoPersist(AppDbContext contexto) : base(contexto)
        {
        }

        public async Task<IList<Produto>> GetAllAsync(int top)
        {
            IQueryable<Produto> query = _contexto.Produtos
                 .Take(top)
                 .OrderByDescending(p => p.Id)
                 .AsNoTracking();

            return await query.ToListAsync();
        }

        public async Task<Produto?> GetByIdAsync(int? id = 0)
        {
            IQueryable<Produto> query = _contexto.Produtos
                .Where(p => p.Id == id)
                .Include(p => p.Categoria)                
                .AsNoTracking();

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Produto?> GetByNameAsync(string nome)
        {
            IQueryable<Produto> query = _contexto.Produtos
                .Where(p => p.Nome.Equals(nome))
                .Include(p => p.Categoria)
                .AsNoTracking();

            return await query.FirstOrDefaultAsync();
        }

        public async Task<IList<Produto>> SearchByNameAsync(string nome)
        {
            IQueryable<Produto> query = _contexto.Produtos
                .Where(p => p.Nome.Contains(nome))
                .AsNoTracking();

            return await query.ToListAsync();
        }
    }
}
