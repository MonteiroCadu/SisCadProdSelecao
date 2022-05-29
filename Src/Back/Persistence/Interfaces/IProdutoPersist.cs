using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Interfaces
{
    public interface IProdutoPersist : IGeralPersist
    {
        Task<IList<Produto>> GetAllAsync(int top);
        Task<Produto?> GetByIdAsync(int? id = 0);
        Task<IList<Produto>> SearchByNameAsync(string nome);
        Task<Produto?> GetByNameAsync(string nome);

    }
}
