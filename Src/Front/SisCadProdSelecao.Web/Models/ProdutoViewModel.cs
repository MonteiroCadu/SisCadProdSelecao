namespace SisCadProdSelecao.Web.Models
{
    public class ProdutoViewModel
    {
        public Produto Produto { get; set; } = null!;
        public List<Categoria>? Categorias { get; set; } 

    }
}
