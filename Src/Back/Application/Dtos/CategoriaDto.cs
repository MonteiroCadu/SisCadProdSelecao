using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class CategoriaDto
    {
        public CategoriaDto()
        {
            Produtos = new HashSet<ProdutoDto>();
        }

        [Key]
        public int? Id { get; set; }

        [MaxLength(50)]
        [Required]
        public string Nome { get; set; } = null!;

        [MaxLength(200)]
        public string? Descricao { get; set; }
        public virtual ICollection<ProdutoDto> Produtos { get; set; }
    }
}
