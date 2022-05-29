using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public partial class AppDbContext : DbContext    
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Produto> Produtos { get; set; } = null!;
        public virtual DbSet<Categoria> Categorias { get; set; } = null!;
    }
}
