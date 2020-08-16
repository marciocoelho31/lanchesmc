using LanchesMC.Models;
using LanchesMC.Repositories;
using LanchesMC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LanchesMC.Controllers
{
    public class LancheController : Controller
    {
        private readonly ILancheRepository _lancheRepository;
        private readonly ICategoriaRepository _categoriaRepository;

        public LancheController(ILancheRepository lancheRepository, ICategoriaRepository categoriaRepository)
        {
            _lancheRepository = lancheRepository;
            _categoriaRepository = categoriaRepository;
        }

        //public IActionResult List()
        //{
        //    ViewBag.Lanche = "Lanches";
        //    ViewData["Categoria"] = "Categoria";

        //    var lanches = _lancheRepository.Lanches;
        //    return View(lanches);
        //}

        //public IActionResult List()
        //{
        //    ViewBag.Lanche = "Lanches";
        //    ViewData["Categoria"] = "Categoria";

        //    var lancheListViewModel = new LancheListViewModel();
        //    lancheListViewModel.Lanches = _lancheRepository.Lanches;
        //    lancheListViewModel.CategoriaAtual = "Categoria Atual";
        //    return View(lancheListViewModel);
        //}

        public IActionResult List(string categoria)
        {
            string _categoria = categoria;
            IEnumerable<Lanche> lanches;
            string categoriaAtual = string.Empty;

            if (string.IsNullOrEmpty(categoria))
            {
                lanches = _lancheRepository.Lanches.OrderBy(l => l.LancheId);
                categoria = "Todos os lanches";
            }
            else
            {
                if (string.Equals("Normal", _categoria, StringComparison.OrdinalIgnoreCase))
                {
                    lanches = _lancheRepository.Lanches
                        .Where(l => l.Categoria.CategoriaNome == "Normal").OrderBy(l=>l.Nome);
                }
                else if (string.Equals("bebidas", _categoria, StringComparison.OrdinalIgnoreCase))
                {
                    lanches = _lancheRepository.Lanches
                        .Where(l => l.Categoria.CategoriaNome == "Bebidas").OrderBy(l => l.Nome);
                }
                else
                {
                    lanches = _lancheRepository.Lanches
                        .Where(l => l.Categoria.CategoriaNome == "Natural").OrderBy(l => l.Nome);
                }

                categoriaAtual = _categoria;
            }

            var lancheListViewModel = new LancheListViewModel
            {
                Lanches = lanches,
                CategoriaAtual = categoriaAtual
            };
            //lancheListViewModel.Lanches = lanches;
            //lancheListViewModel.CategoriaAtual = categoriaAtual;

            return View(lancheListViewModel);
        }

        public IActionResult Details(int lancheId)
        {
            var lanche = _lancheRepository.Lanches.FirstOrDefault(l => l.LancheId == lancheId);
            if (lanche == null)
            {
                return View("~/Views/Error/Error.cshtml");
            }
            return View(lanche);
        }

        public IActionResult Search(string searchString)
        {
            string _searchString = searchString;
            IEnumerable<Lanche> lanches;
            string _categoriaAtual = string.Empty;

            if (string.IsNullOrEmpty(_searchString))
            {
                lanches = _lancheRepository.Lanches.OrderBy(l => l.LancheId);
            }
            else
            {
                lanches = _lancheRepository.Lanches.Where(l => l.Nome.ToLower().Contains(_searchString.ToLower()));
            }

            return View("~/Views/Lanche/List.cshtml", new LancheListViewModel { Lanches = lanches, CategoriaAtual = "Todos os lanches" });
        }

    }
}
