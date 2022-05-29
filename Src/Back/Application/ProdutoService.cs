using Application.Dtos;
using Application.Interfaces;
using AutoMapper;
using Domain;
using Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoPersist produtoPersist;
        private readonly IMapper mapper;

        public ProdutoService(IProdutoPersist produtoPersist, IMapper mapper)
        {
            this.produtoPersist = produtoPersist;
            this.mapper = mapper;
        }

        public async Task<IList<ProdutoDto>> GetAllAsync(int top)
        {
            var produtosModel = await this.produtoPersist.GetAllAsync(top);
            var produtosDto = this.mapper.Map<IList<ProdutoDto>>(produtosModel);

            return produtosDto;
        }

        public async Task<ProdutoDto?> GetByIdAsync(int? id = 0)
        {
            var produtoModel = await this.produtoPersist.GetByIdAsync(id);
            ProdutoDto produtoDto;


            if (produtoModel != null)
            {
                produtoDto = this.mapper.Map<ProdutoDto>(produtoModel);
                return produtoDto;
            }

            return null;
        }
        

        public async Task<IList<ProdutoDto>> SearchByNameAsync(string nome)
        {
            var produtoModel = await this.produtoPersist.SearchByNameAsync(nome);

            var produtosDto = this.mapper.Map<IList<ProdutoDto>>(produtoModel);

            return produtosDto;
        }

        public async Task<ProdutoDto?> save(ProdutoDto produtoDto)
        {
            if (produtoDto == null)
                throw new Exception("Erro ao salvar produto, objeto nulo");


            var produtoModel = this.mapper.Map<Produto>(produtoDto);
            var produtoBuscadoPorNome = await this.produtoPersist.GetByNameAsync(produtoDto.Nome);
            

            var msgErrorNomeCadastrado = "Nome de produto já cadastrado para outro produto cadastrado";
           

            if (produtoDto.Id == null || produtoDto.Id == 0)
            {
                if (produtoBuscadoPorNome != null) throw new Exception(msgErrorNomeCadastrado);                

                this.produtoPersist.Add(produtoModel);
            }
            else
            {
                var produtoBuscadoPorId = await this.produtoPersist.GetByIdAsync(produtoDto.Id);
                if (produtoBuscadoPorId == null) throw new Exception("Id do produto não cadastrado no sistema");
                if (produtoBuscadoPorNome != null && produtoBuscadoPorNome?.Id != produtoDto.Id) throw new Exception(msgErrorNomeCadastrado);
                              
                this.produtoPersist.Update(produtoModel);
            }


            try
            {
                if (await this.produtoPersist.SaveChangesAsync())
                {
                    return await this.GetByIdAsync(produtoModel.Id);
                }
                return null;

            }
            catch (System.Exception ex)
            {

                throw new Exception($"Erro não mapeado ao salvar produto no banco de dados{ex.Message}");
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var produto = await this.produtoPersist.GetByIdAsync(id);
                if (produto == null) throw new Exception("Produto para delete não encontrado");


                this.produtoPersist.Delete<Produto>(produto);
                return await this.produtoPersist.SaveChangesAsync();
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
