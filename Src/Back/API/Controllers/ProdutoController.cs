using Application.Dtos;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api.v1/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoService produtoService;
        private readonly IConfiguration configuration;
        private int paginationSize;

        public ProdutoController(IProdutoService produtoService, IConfiguration configuration)
        {
            this.produtoService = produtoService;
            this.configuration = configuration;
            
            this.paginationSize = 100;
            Int32.TryParse(configuration.GetSection("paginationSize")?.Value, out paginationSize);
        }
        // GET: api/<ProdutoController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var produtos = await this.produtoService.GetAllAsync(paginationSize);
                if (produtos == null) return NoContent();

                return Ok(produtos);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar produtos. Erro:{ex.Message}");
            }
        }

        // GET api/<ProdutoController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var produto = await this.produtoService.GetByIdAsync(id);
                if (produto == null) return NoContent();

                return Ok(produto);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar o produto. Erro:{ex.Message}");
            }
        }

        // POST api/<ProdutoController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProdutoDto categoria)
        {
            try
            {
                var produtoSaved = await this.produtoService.save(categoria);
                if (produtoSaved == null) return BadRequest("Erro ao tentar adicionar o produto");

                return Ok(produtoSaved);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar adicionar o produto. Erro:{ex.Message}");
            }
        }

        // PUT api/<ProdutoController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ProdutoDto produto)
        {
            try
            {
                var produtoSaved = await this.produtoService.save(produto);
                if (produtoSaved == null) return BadRequest("Erro ao tentar adicionar o produto");

                return Ok(produtoSaved);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar adicionar a categoria. Erro:{ex.Message}");
            }
        }

        // DELETE api/<ProdutoController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var produto = await this.produtoService.GetByIdAsync(id);
                if (produto == null) return NoContent();

                return await this.produtoService.Delete(id)
                        ? Ok(new { message = "Deletado" })
                        : throw new System.Exception($"Erro não espessifico ao tentar deletar o produto Id: {id}");


            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar deletar o evento. Erro:{ex.Message}");
            }
        }
    }
}
