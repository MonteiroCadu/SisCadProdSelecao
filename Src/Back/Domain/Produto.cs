using System.ComponentModel.DataAnnotations;


namespace Domain
{
    public class Produto
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        [Required]
        public string Nome { get; set; } = string.Empty;
        
        [MaxLength(200)]
        public string Descricao { get; set; }  = string.Empty;

        public double Estoque { get; set; } = 0;
        public int CategoriaId { get; set; }
        public virtual Categoria Categoria { get; set; } = null!;
    }
}
