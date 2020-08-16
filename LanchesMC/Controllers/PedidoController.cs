using LanchesMC.Models;
using LanchesMC.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace LanchesMC.Controllers
{
    public class PedidoController : Controller
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly CarrinhoCompra _carrinhoCompra;

        public PedidoController(IPedidoRepository pedidoRepository, CarrinhoCompra carrinhoCompra)
        {
            _pedidoRepository = pedidoRepository;
            _carrinhoCompra = carrinhoCompra;
        }

        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Checkout(Pedido pedido)        // dados vem via model-binding (recurso .net)
        {
            //var items = _carrinhoCompra.GetCarrinhoCompraItens();
            //_carrinhoCompra.CarrinhoCompraItens = items;

            //if (_carrinhoCompra.CarrinhoCompraItens.Count == 0)
            //{
            //    ModelState.AddModelError("", "Seu carrinho está vazio, inclua um lanche...");
            //}

            //if (ModelState.IsValid)
            //{
            //    _pedidoRepository.CriarPedido(pedido);

            //    //TempData["Cliente"] = pedido.Nome;
            //    //TempData["NumeroPedido"] = pedido.PedidoId;
            //    //TempData["DataPedido"] = pedido.PedidoEnviado;
            //    //TempData["TotalPedido"] = _carrinhoCompra.GetCarrinhoCompraTotal().ToString("C2");

            //    ViewBag.CheckoutCompletoMensagem = "Obrigado pelo seu pedido! :)";
            //    ViewBag.TotalPedido = _carrinhoCompra.GetCarrinhoCompraTotal().ToString("C2");

            //    _carrinhoCompra.LimparCarrinho();

            //    //return RedirectToAction("CheckoutCompleto");
            //    return View("~/Views/Pedido/CheckoutCompleto.cshtml", pedido);
            //}

            //return View(pedido);

            decimal precoTotalPedido = 0.0m;
            int totalItensPedido = 0;

            List<CarrinhoCompraItem> items = _carrinhoCompra.GetCarrinhoCompraItens();

            _carrinhoCompra.CarrinhoCompraItens = items;

            //verifica se existem itens de pedidos
            if (_carrinhoCompra.CarrinhoCompraItens.Count == 0)
            {
                ModelState.AddModelError("", "Seu carrinho esta vazio, que tal incluir um lanche...");
            }

            //calcula o total do pedido
            foreach (var item in items)
            {
                totalItensPedido += item.Quantidade;
                precoTotalPedido += (item.Lanche.Preco * item.Quantidade);
            }

            //atribui o total de itens do pedido
            pedido.TotalItensPedido = totalItensPedido;

            //atribui o total do pedido ao pedido
            pedido.PedidoTotal = precoTotalPedido;

            if (ModelState.IsValid)
            {
                _pedidoRepository.CriarPedido(pedido);

                ViewBag.CheckoutCompletoMensagem = "Obrigado pelo seu pedido :) ";
                ViewBag.TotalPedido = _carrinhoCompra.GetCarrinhoCompraTotal();

                _carrinhoCompra.LimparCarrinho();
                return View("~/Views/Pedido/CheckoutCompleto.cshtml", pedido);
            }
            return View(pedido);

        }

        //public IActionResult CheckoutCompleto()
        //{
        //    ViewBag.CheckoutCompletoMensagem = "Obrigado pelo seu pedido! :)";

        //    ViewBag.Cliente = TempData["Cliente"];
        //    ViewBag.DataPedido = TempData["DataPedido"];
        //    ViewBag.NumeroPedido = TempData["NumeroPedido"];
        //    ViewBag.TotalPedido = TempData["TotalPedido"];

        //    return View();
        //}

    }
}
