using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SisCadProdSelecao.Web.Models;
using SisCadProdSelecao.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SisCadProdSelecao.Web.Controllers
{
    public class ProdutoController : Controller
    {
       
        private readonly IProdutoService produtoService;
        private readonly ICategoriaService categoriaService;

        public ProdutoController(IProdutoService produtoService,ICategoriaService categoriaService) : base()
        {
            this.produtoService = produtoService;
            this.categoriaService = categoriaService;
        }
        // GET: ProdutoController
        public async Task<IActionResult> List(string stringBuscaPessoa)
        {                       

            this.SetViewData("Listagem", false);                      
            
            var produtos = stringBuscaPessoa == null
                            ? await this.produtoService.GetAllAsync()
                            : await this.produtoService.SearchByNameAsync(stringBuscaPessoa);

            ViewData["stringBuscaPessoa"] = stringBuscaPessoa;
            return View(produtos);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var produtoSalvo = await this.produtoService.GetByIdAsync(id);
            
            if (produtoSalvo == null) return BadRequest("Produto não cadastrado no sistema");
            
            ViewData["Id"] = produtoSalvo?.Id;

            this.SetViewData("Edição", true);
            
            
            if (produtoSalvo == null) return View("List");
            var categorias = await this.categoriaService.GetAllAsync();

            var produtoViewModel = new ProdutoViewModel {
                Produto = produtoSalvo,
                Categorias = categorias
            };               

            return View("Detalhe", produtoViewModel);
        }

        public async Task<ActionResult> Create()
        {
            this.SetViewData("Cadastro", false);

            var categorias = await this.categoriaService.GetAllAsync();

            var produtoViewModel = new ProdutoViewModel
            {
                Produto = new Produto(),
                Categorias = categorias
            };

            return View("Detalhe", produtoViewModel);
        }

        public async Task<ActionResult> Save(Produto produto)
        {
            Produto? produtoSalvo = produto;


            if (ModelState.IsValid)
            {
                try
                {
                    produtoSalvo = await this.produtoService.save(produtoSalvo);
                    return RedirectToAction("Edit", new { id = produtoSalvo?.Id });
                }
                catch (Exception e)
                {
                    ViewData["Error"] = e.Message;
                }

            }


            ViewData["Id"] = produtoSalvo?.Id;
            var subtitulo = "";
            var edicao = false;

            if (produtoSalvo?.Id == 0)
                subtitulo = "Cadastro";
            else
            {
                subtitulo = "Edição";
                edicao = true;
            }

            this.SetViewData(subtitulo, edicao);

            if (produtoSalvo == null) return View("List");
            var categorias = await this.categoriaService.GetAllAsync();

            var produtoViewModel = new ProdutoViewModel
            {
                Produto = produtoSalvo,
                Categorias = categorias
            };

            return View("Detalhe", produtoViewModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var produtoSalvo = await this.produtoService.GetByIdAsync(id);            
            if (produtoSalvo == null) return BadRequest("Produto não cadastrado no sistema");

            await this.produtoService.Delete(id);

            return RedirectToAction("List");
        }

        private void SetViewData(string subTitulo, bool edicao)
        {
            ViewData["Titulo"] = "Produtos";
            ViewData["SubTitulo"] = subTitulo;
            ViewData["Edicao"] = edicao;
            ViewData["AspController"] = "Produto";
            ViewData["AspAction"] = "List";
            ViewData["FontIcon"] = "fa-users";
        }
    }
}
