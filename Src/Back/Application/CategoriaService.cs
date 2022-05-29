using Application.Dtos;
using Application.Interfaces;
using AutoMapper;
using Domain;
using Persistence.Interfaces;


namespace Application
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaPersist categoriaPersist;
        private readonly IMapper mapper;

        public CategoriaService(ICategoriaPersist categoriaPersist, IMapper mapper)
        {
            this.categoriaPersist = categoriaPersist;
            this.mapper = mapper;
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var categoria = await this.categoriaPersist.GetByIdAsync(id);
                if (categoria == null) throw new Exception("Categoria para delete não encontrado");


                this.categoriaPersist.Delete<Categoria>(categoria);
                return await this.categoriaPersist.SaveChangesAsync();
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IList<CategoriaDto>> GetAllAsync(int top)
        {
            var categoriaModel = await this.categoriaPersist.GetAllAsync(top);
            var categoriaDto = this.mapper.Map<IList<CategoriaDto>>(categoriaModel);

            return categoriaDto;
        }

        public async Task<CategoriaDto?> GetByIdAsync(int? id = 0)
        {
            var categoriaModel = await this.categoriaPersist.GetByIdAsync(id);
            CategoriaDto categoriaDto;


            if (categoriaModel != null)
            {
                categoriaDto = this.mapper.Map<CategoriaDto>(categoriaModel);
                return categoriaDto;
            }

            return null;
        }

        public async Task<CategoriaDto?> save(CategoriaDto categoriaDto)
        {
            if (categoriaDto == null)
                throw new Exception("Erro ao salvar produto, objeto nulo");


            var categoriaModel = this.mapper.Map<Categoria>(categoriaDto);
            var categoriaBuscadaPorNome = await this.categoriaPersist.GetByNameAsync(categoriaDto.Nome);


            var msgErrorNomeCadastrado = "Nome de categoria já cadastrado para outra categoria cadastrada";


            if (categoriaDto.Id == null || categoriaDto.Id == 0)
            {
                if (categoriaBuscadaPorNome != null) throw new Exception(msgErrorNomeCadastrado);

                this.categoriaPersist.Add(categoriaModel);
            }
            else
            {
                var categoriaBuscadaPorId = await this.categoriaPersist.GetByIdAsync(categoriaDto.Id);
                if (categoriaBuscadaPorId == null) throw new Exception("Id da categoria não cadastrado no sistema");
                if (categoriaBuscadaPorNome != null && categoriaBuscadaPorNome.Id != categoriaDto.Id) throw new Exception(msgErrorNomeCadastrado);

                this.categoriaPersist.Update(categoriaModel);
            }


            try
            {
                if (await this.categoriaPersist.SaveChangesAsync())
                {
                    return await this.GetByIdAsync(categoriaModel.Id);
                }
                return null;

            }
            catch (System.Exception ex)
            {

                throw new Exception($"Erro não mapeado ao salvar categoria no banco de dados{ex.Message}");
            }
        }

        public async Task<IList<CategoriaDto>> SearchByNameAsync(string nome)
        {
            var categoriasModel = await this.categoriaPersist.SearchByNameAsync(nome);

            var categoriasDto = this.mapper.Map<IList<CategoriaDto>>(categoriasModel);

            return categoriasDto;
        }
    }
}
