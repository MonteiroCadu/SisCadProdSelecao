using System.ComponentModel.DataAnnotations;

namespace SisCadProdSelecao.Web.Models
{
    public class Categoria
    {
        
        public int? Id { get; set; }
        [Required,MaxLength(50)]
        public string Nome { get; set; } = null!;
        [MaxLength(200)]
        public string? Descricao { get; set; }
       
    }
}
