using Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IProdutoService
    {
        Task<IList<ProdutoDto>> GetAllAsync(int top);
        Task<ProdutoDto?> GetByIdAsync(int? id = 0);
        Task<IList<ProdutoDto>> SearchByNameAsync(string nome);
        Task<ProdutoDto?> save(ProdutoDto produtoDto);
        Task<bool> Delete(int id);
    }
}
