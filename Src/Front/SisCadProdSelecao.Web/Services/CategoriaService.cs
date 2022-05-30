using SisCadProdSelecao.Web.Models;

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
            var produtos = await retorno.Content.ReadFromJsonAsync<List<Categoria>>();

            return produtos;
        }

        public Task<Categoria?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Categoria>?> SearchByNameAsync(string Name)
        {
            throw new NotImplementedException();
        }
    }
}
