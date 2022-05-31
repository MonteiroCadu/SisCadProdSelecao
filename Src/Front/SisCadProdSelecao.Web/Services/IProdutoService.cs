using Microsoft.AspNetCore.Mvc;
using SisCadProdSelecao.Web.Models;
using System.Net;

namespace SisCadProdSelecao.Web.Services
{
    public interface IProdutoService
    {
        public Task<Produto?> GetByIdAsync(int id);
        public Task<List<Produto>?> GetAllAsync();
        public Task<List<Produto>?> SearchByNameAsync(string Name);
        public Task<Produto?> save(Produto produto);
        public Task<HttpStatusCode> Delete(int id);
    }
}
