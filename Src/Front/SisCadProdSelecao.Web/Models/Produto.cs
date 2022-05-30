using System.ComponentModel.DataAnnotations;

namespace SisCadProdSelecao.Web.Models
{
    public class Produto
    {
        public int? Id { get; set; }
        [Required, MaxLength(50)]
        public string Nome { get; set; } = null!;
        [MaxLength(50)]
        public string? Descricao { get; set; }
        public double Estoque { get; set; }
        public int CategoriaId { get; set; }
        public Categoria? Categoria { get; set; }
    }
}
