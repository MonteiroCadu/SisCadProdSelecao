using SisCadProdSelecao.Web.Models;

namespace SisCadProdSelecao.Web.Services
{
    public interface IProdutoService
    {
        public Task<Produto?> GetByIdAsync(int id);
        public Task<List<Produto>?> GetAllAsync();
        public Task<List<Produto>?> SearchByNameAsync(string Name);
    }
}
