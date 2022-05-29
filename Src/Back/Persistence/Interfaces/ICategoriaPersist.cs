using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Interfaces
{
    public interface ICategoriaPersist : IGeralPersist
    {
        Task<IList<Categoria>> GetAllAsync(int top);
        Task<Categoria?> GetByIdAsync(int? id = 0);
        Task<IList<Categoria>> SearchByNameAsync(string nome);
        Task<Categoria?> GetByNameAsync(string nome);
    }
}
