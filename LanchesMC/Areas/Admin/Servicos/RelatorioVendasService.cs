using LanchesMC.Context;
using LanchesMC.Models;
using LanchesMC.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanchesMC.Areas.Admin.Servicos
{
    public class RelatorioVendasService
    {
        private readonly AppDbContext context;
        public RelatorioVendasService(AppDbContext _context)
        {
            context = _context;
        }

        public async Task<List<Pedido>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            var resultado = from obj in context.Pedidos select obj;

            if (minDate.HasValue)
            {
                resultado = resultado.Where(x => x.PedidoEnviado >= minDate.Value);
            }
            
            if (maxDate.HasValue)
            {
                resultado = resultado.Where(x => x.PedidoEnviado <= maxDate.Value);
            }

            // inclui no resultado do relatorio, alem dos pedidos acima, os itens e as relacoes com os lanches
            return await resultado
                .Include(l => l.PedidoItens)
                .ThenInclude(l=> l.Lanche)
                .OrderByDescending(x => x.PedidoEnviado)
                .ToListAsync();
        }
    }
}
