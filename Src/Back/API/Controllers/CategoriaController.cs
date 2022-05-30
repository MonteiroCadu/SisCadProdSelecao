using Application.Dtos;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api.v1/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService categoriaService;
        private readonly IConfiguration configuration;
        private int paginationSize;

        public CategoriaController(ICategoriaService categoriaService, IConfiguration configuration)
        {
            this.categoriaService = categoriaService;
            this.configuration = configuration;

            this.paginationSize = 100;
            Int32.TryParse(configuration.GetSection("paginationSize")?.Value, out paginationSize);
        }

        // GET: api/<CategoriaController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {               

                var categorias = await categoriaService.GetAllAsync(paginationSize);
                if (categorias == null) return NoContent();

                return Ok(categorias);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar categorias. Erro:{ex.Message}");
            }
        }

        // GET api/<CategoriaController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var categoria = await categoriaService.GetByIdAsync(id);
                if (categoria == null) return NoContent();

                return Ok(categoria);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar a categoria. Erro:{ex.Message}");
            }
        }

        [HttpGet("Nome/{nome}")]
        public async Task<IActionResult> Get(string nome)
        {
            try
            {
                var categoria = await categoriaService.SearchByNameAsync(nome);
                if (categoria == null) return NoContent();

                return Ok(categoria);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar as categorias. Erro:{ex.Message}");
            }
        }

        // POST api/<CategoriaController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CategoriaDto categoria)
        {
            try
            {
                var categoriaSaved = await this.categoriaService.save(categoria);
                if (categoriaSaved == null) return BadRequest("Erro ao tentar adicionar a categoria");

                return Ok(categoriaSaved);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar adicionar o produto. Erro:{ex.Message}");
            }
        }

        // PUT api/<CategoriaController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CategoriaDto categoria)
        {
            try
            {
                var categoriaSaved = await this.categoriaService.save(categoria);
                if (categoriaSaved == null) return BadRequest("Erro ao tentar adicionar a categoria");

                return Ok(categoriaSaved);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar adicionar a categoria. Erro:{ex.Message}");
            }
        }

        // DELETE api/<CategoriaController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var categoria = await this.categoriaService.GetByIdAsync(id);
                if (categoria == null) return NoContent();

                return await this.categoriaService.Delete(id)
                        ? Ok(new { message = "Deletado" })
                        : throw new System.Exception($"Erro não espessifico ao tentar deletar a categoria Id: {id}");


            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar deletar o evento. Erro:{ex.Message}");
            }
        }
    }
}
