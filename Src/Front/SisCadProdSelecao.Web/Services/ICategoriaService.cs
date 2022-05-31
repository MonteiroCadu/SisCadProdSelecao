using SisCadProdSelecao.Web.Models;
using System.Net;

namespace SisCadProdSelecao.Web.Services
{
    public interface ICategoriaService
    {
        public Task<Categoria?> GetByIdAsync(int id);
        public Task<List<Categoria>?> GetAllAsync();
        public Task<List<Categoria>?> SearchByNameAsync(string Name);

        public Task<Categoria?> save(Categoria produto);
        public Task<HttpStatusCode> Delete(int id);
    }
}
