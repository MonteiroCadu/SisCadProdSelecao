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

            return View(produtos);
        }

        public async Task<IActionResult> Edit(int id)
        {

            this.SetViewData("Edição", true);

            var produto = await this.produtoService.GetByIdAsync(id);
            var categorias = await this.categoriaService.GetAllAsync();
            if (produto == null) return View("List");

            var produtoViewModel = new ProdutoViewModel {
                Produto = produto,
                Categorias = categorias
            };               

            return View("Detalhe", produtoViewModel);
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
