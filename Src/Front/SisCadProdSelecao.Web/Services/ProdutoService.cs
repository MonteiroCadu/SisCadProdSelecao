using SisCadProdSelecao.Web.Models;

namespace SisCadProdSelecao.Web.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IConfiguration configuration;
        private readonly HttpClient httpClient;

        public ProdutoService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            this.httpClientFactory = httpClientFactory;
            this.configuration = configuration;

            var apiHttpClient = configuration.GetSection("SisCadApi").Value;
            this.httpClient = httpClientFactory.CreateClient(apiHttpClient);
        }

        public async Task<List<Produto>?> GetAllAsync()
        {
            var retorno = await httpClient.GetAsync("Produto");
            var produtos = await retorno.Content.ReadFromJsonAsync<List<Produto>>();
            
            return produtos;
        }

        public async Task<Produto?> GetByIdAsync(int id)
        {
            var retorno = await httpClient.GetAsync($"Produto/{id}");
            var produto = await retorno.Content.ReadFromJsonAsync<Produto>();

            return produto;
        }

        public async Task<List<Produto>?> SearchByNameAsync(string nome)
        {
            var retorno = await httpClient.GetAsync($"Produto/Nome/{nome}");
            var produtos = await retorno.Content.ReadFromJsonAsync<List<Produto>>();

            return produtos;
        }
    }
}
