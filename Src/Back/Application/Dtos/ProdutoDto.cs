using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class ProdutoDto
    {
        [Key]
        public int? Id { get; set; }

        [MaxLength(50)]
        [Required]
        public string Nome { get; set; } = string.Empty;

        [MaxLength(200)]
        public string Descricao { get; set; } = string.Empty;

        public double Estoque { get; set; } = 0;
        public int CategoriaId { get; set; }
        public virtual CategoriaDto? Categoria { get; set; }
    }
}
