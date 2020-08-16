using LanchesMC.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;

namespace LanchesMC.Models
{
    public class CarrinhoCompra
    {
        private readonly AppDbContext _contexto;

        public CarrinhoCompra(AppDbContext contexto)
        {
            _contexto = contexto;
        }
        public string CarrinhoCompraId { get; set; }
        public List<CarrinhoCompraItem> CarrinhoCompraItens { get; set; }

        public static CarrinhoCompra GetCarrinho(IServiceProvider services)
        {
            // define uma sessao acessando o contexto atual (tem que registrar em IServicesCollection)
            ISession session =
                services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            // obtem um serviço do tipo do nosso contexto
            var context = services.GetService<AppDbContext>();

            // obtem ou gera o Id do carrinho
            string carrinhoId = session.GetString("CarrinhoId") ?? Guid.NewGuid().ToString();

            // atribui o id do carrinho na sessão
            session.SetString("CarrinhoId", carrinhoId);

            // retorna o carrinho com o contexto atual e o Id atribuido ou obtido
            return new CarrinhoCompra(context)
            {
                CarrinhoCompraId = carrinhoId
            };
        }

        public void AdicionarAoCarrinho(Lanche lanche, int quantidade)
        {
            var carrinhoCompraItem =
                _contexto.CarrinhoCompraItens.SingleOrDefault(
                    s => s.Lanche.LancheId == lanche.LancheId && s.CarrinhoCompraId == CarrinhoCompraId);

            // verifica se o carrinho existe e senao existir cria um
            if (carrinhoCompraItem == null)
            {
                carrinhoCompraItem = new CarrinhoCompraItem
                {
                    CarrinhoCompraId = CarrinhoCompraId,
                    Lanche = lanche,
                    Quantidade = 1
                };
                _contexto.CarrinhoCompraItens.Add(carrinhoCompraItem);
            }
            else // se existir o carrinho com o item entao incrementa a quantidade
            {
                carrinhoCompraItem.Quantidade++;
            }
            _contexto.SaveChanges();
        }

        public int RemoverDoCarrinho(Lanche lanche)
        {
            var carrinhoCompraItem =
                _contexto.CarrinhoCompraItens.SingleOrDefault(
                    s => s.Lanche.LancheId == lanche.LancheId && s.CarrinhoCompraId == CarrinhoCompraId);

            var quantidadeLocal = 0;

            if (carrinhoCompraItem != null)
            {
                if (carrinhoCompraItem.Quantidade > 1)
                {
                    carrinhoCompraItem.Quantidade--;
                    quantidadeLocal = carrinhoCompraItem.Quantidade;
                }
                else
                {
                    _contexto.CarrinhoCompraItens.Remove(carrinhoCompraItem);
                }
            }

            _contexto.SaveChanges();

            return quantidadeLocal;
        }

        public List<CarrinhoCompraItem> GetCarrinhoCompraItens()
        {
            return CarrinhoCompraItens ??
                (CarrinhoCompraItens =
                    _contexto.CarrinhoCompraItens.Where(c => c.CarrinhoCompraId == CarrinhoCompraId)
                        .Include(s => s.Lanche)
                        .ToList());
        }

        public void LimparCarrinho()
        {
            var carrinhoItens = _contexto.CarrinhoCompraItens
                                    .Where(carrinho => carrinho.CarrinhoCompraId == CarrinhoCompraId);

            _contexto.CarrinhoCompraItens.RemoveRange(carrinhoItens);

            _contexto.SaveChanges();
        }

        public decimal GetCarrinhoCompraTotal()
        {
            var total = _contexto.CarrinhoCompraItens.Where(c => c.CarrinhoCompraId == CarrinhoCompraId)
                .Select(c => c.Lanche.Preco * c.Quantidade).Sum();

            return total;
        }

    }
}
