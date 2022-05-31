using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SisCadProdSelecao.Web.Models;
using SisCadProdSelecao.Web.Services;

namespace SisCadProdSelecao.Web.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly ICategoriaService categoriaService;

        public CategoriaController(ICategoriaService categoriaService)
        {
            this.categoriaService = categoriaService;
        }

        public async Task<IActionResult> List(string stringBuscaCategoria)
        {

            this.SetViewData("Listagem", false);

            var categorias = stringBuscaCategoria == null
                            ? await this.categoriaService.GetAllAsync()
                            : await this.categoriaService.SearchByNameAsync(stringBuscaCategoria);

            ViewData["stringBuscaCategoria"] = stringBuscaCategoria;
            return View(categorias);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var categoriaSalvo = await this.categoriaService.GetByIdAsync(id);

            if (categoriaSalvo == null) return BadRequest("Categoria não cadastrado no sistema");

            ViewData["Id"] = categoriaSalvo?.Id;

            this.SetViewData("Edição", true);


            if (categoriaSalvo == null) return View("List");

            return View("Detalhe", categoriaSalvo);
        }

        public ActionResult Create()
        {
            this.SetViewData("Cadastro", false);            

            return View("Detalhe", new Categoria());
        }

        public async Task<ActionResult> Save(Categoria categoria)
        {
            Categoria? categoriaSalvo = categoria;


            if (ModelState.IsValid)
            {
                try
                {
                    categoriaSalvo = await this.categoriaService.save(categoriaSalvo);
                    return RedirectToAction("Edit", new { id = categoriaSalvo?.Id });
                }
                catch (Exception e)
                {
                    ViewData["Error"] = e.Message;
                }

            }


            ViewData["Id"] = categoriaSalvo?.Id;
            var subtitulo = "";
            var edicao = false;

            if (categoriaSalvo?.Id == 0)
                subtitulo = "Cadastro";
            else
            {
                subtitulo = "Edição";
                edicao = true;
            }

            this.SetViewData(subtitulo, edicao);

            if (categoriaSalvo == null) return View("List");
            

            return View("Detalhe", categoriaSalvo);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var categoriaSalvo = await this.categoriaService.GetByIdAsync(id);
            if (categoriaSalvo == null) return BadRequest("categoria não cadastrada no sistema");

            await this.categoriaService.Delete(id);

            return RedirectToAction("List");
        }

        private void SetViewData(string subTitulo, bool edicao)
        {
            ViewData["Titulo"] = "Categorias";
            ViewData["SubTitulo"] = subTitulo;
            ViewData["Edicao"] = edicao;
            ViewData["AspController"] = "Categoria";
            ViewData["AspAction"] = "List";
            ViewData["FontIcon"] = "fa-users";
        }
    }
}
