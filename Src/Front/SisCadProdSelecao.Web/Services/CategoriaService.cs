using SisCadProdSelecao.Web.Models;
using System.Net;

namespace SisCadProdSelecao.Web.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IConfiguration configuration;
        private readonly HttpClient httpClient;

        public CategoriaService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            this.httpClientFactory = httpClientFactory;
            this.configuration = configuration;

            var SisCadApi = configuration.GetSection("SisCadApi").Value;
            this.httpClient = httpClientFactory.CreateClient(SisCadApi);
        }
        

        public async Task<List<Categoria>?> GetAllAsync()
        {
            var retorno = await httpClient.GetAsync("Categoria");
            if (!retorno.IsSuccessStatusCode) return null;
            var produtos = await retorno.Content.ReadFromJsonAsync<List<Categoria>>();

            return produtos;
        }

        public async Task<Categoria?> GetByIdAsync(int id)
        {
            var retorno = await httpClient.GetAsync($"Categoria/{id}");
            if (!retorno.IsSuccessStatusCode) return null;
            var categoria = await retorno.Content.ReadFromJsonAsync<Categoria>();

            return categoria;
        }

        public async Task<List<Categoria>?> SearchByNameAsync(string Name)
        {
            var retorno = await httpClient.GetAsync($"Categoria/Nome/{Name}");
            if (!retorno.IsSuccessStatusCode) return null;
            var categorias = await retorno.Content.ReadFromJsonAsync<List<Categoria>>();

            return categorias;
        }

        public async Task<Categoria?> save(Categoria categoria)
        {
            if (categoria == null) return null;

            HttpResponseMessage retorno;

            if (categoria.Id != null && categoria.Id != 0)
                retorno = await httpClient.PutAsJsonAsync<Categoria>($"Categoria/{categoria.Id}", categoria);
            else
                retorno = await httpClient.PostAsJsonAsync<Categoria>($"Categoria", categoria);

            if (!retorno.IsSuccessStatusCode) return null;

            var CategoriaSalvo = await retorno.Content.ReadFromJsonAsync<Categoria>();

            return CategoriaSalvo;
        }

        public async Task<HttpStatusCode> Delete(int id)
        {
            var retorno = await httpClient.DeleteAsync($"Categoria/{id}");
            return retorno.StatusCode;
        }

    }
}
