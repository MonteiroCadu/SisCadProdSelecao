using Microsoft.AspNetCore.Mvc;
using SisCadProdSelecao.Web.Models;
using System.Net;

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
            if (!retorno.IsSuccessStatusCode) return null;
            var produtos = await retorno.Content.ReadFromJsonAsync<List<Produto>>();
            
            return produtos;
        }

        public async Task<Produto?> GetByIdAsync(int id)
        {
            var retorno = await httpClient.GetAsync($"Produto/{id}");
            if (!retorno.IsSuccessStatusCode) return null;
            var produto = await retorno.Content.ReadFromJsonAsync<Produto>();

            return produto;
        }

        public async Task<Produto?> save(Produto produto)
        {
            if(produto == null) return null;

            HttpResponseMessage retorno;

            if(produto.Id != null && produto.Id != 0)            
                retorno = await httpClient.PutAsJsonAsync<Produto>($"Produto/{produto.Id}", produto);            
            else            
                retorno = await httpClient.PostAsJsonAsync<Produto>($"Produto", produto);            

            if (!retorno.IsSuccessStatusCode) return null;

            var produtoSalvo = await retorno.Content.ReadFromJsonAsync<Produto>();

            return produtoSalvo;
        }

        public async Task<List<Produto>?> SearchByNameAsync(string nome)
        {
            var retorno = await httpClient.GetAsync($"Produto/Nome/{nome}");
            if (!retorno.IsSuccessStatusCode) return null;
            var produtos = await retorno.Content.ReadFromJsonAsync<List<Produto>>();

            return produtos;
        }

        public async Task<HttpStatusCode> Delete(int id)
        {
            var retorno = await httpClient.DeleteAsync($"Produto/{id}");
            return retorno.StatusCode;
        }
    }
}
