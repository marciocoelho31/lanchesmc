using LanchesMC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LanchesMC.Context
{

    //public class AppDbContext : DbContext /* "classe de contexto" - definição simples, sem autenticação */
    public class AppDbContext : IdentityDbContext<IdentityUser>     /* dessa forma ele cria no banco de dados as tabelas referentes a autenticacao */
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            /* representação da sessão do banco de dados */
        }

        public DbSet<Lanche> Lanches { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<CarrinhoCompraItem> CarrinhoCompraItens { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<PedidoDetalhe> PedidoDetalhes { get; set; }

    }
}
