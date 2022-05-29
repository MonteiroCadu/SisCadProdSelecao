using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Categoria
    {
        public Categoria()
        {
            Produtos = new HashSet<Produto>();
        }

        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        [Required]
        public string Nome { get; set; } = null!;

        [MaxLength(200)]
        public string? Descricao { get; set; }
        public virtual ICollection<Produto> Produtos { get; set; }
    }
}
