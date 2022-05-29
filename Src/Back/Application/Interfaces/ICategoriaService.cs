using Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICategoriaService
    {
        Task<IList<CategoriaDto>> GetAllAsync(int top);
        Task<CategoriaDto?> GetByIdAsync(int? id = 0);
        Task<IList<CategoriaDto>> SearchByNameAsync(string nome);
        Task<CategoriaDto?> save(CategoriaDto produtoDto);
        Task<bool> Delete(int id);
    }
}
