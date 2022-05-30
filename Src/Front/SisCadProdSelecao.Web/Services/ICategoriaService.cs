using SisCadProdSelecao.Web.Models;

namespace SisCadProdSelecao.Web.Services
{
    public interface ICategoriaService
    {
        public Task<Categoria?> GetByIdAsync(int id);
        public Task<List<Categoria>?> GetAllAsync();
        public Task<List<Categoria>?> SearchByNameAsync(string Name);
    }
}
