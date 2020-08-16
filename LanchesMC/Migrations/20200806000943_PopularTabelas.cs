using Microsoft.EntityFrameworkCore.Migrations;

namespace LanchesMC.Migrations
{
    public partial class PopularTabelas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Categorias(CategoriaNome,Descricao) VALUES('Normal','Lanche feito com ingredientes normais')");
            migrationBuilder.Sql("INSERT INTO Categorias(CategoriaNome,Descricao) VALUES('Natural','Lanche feito com ingredientes integrais e naturais')");
            migrationBuilder.Sql("INSERT INTO Categorias(CategoriaNome,Descricao) VALUES('Bebidas','Bebidas quentes e geladas')");

            migrationBuilder.Sql("INSERT INTO Lanches(CategoriaId,DescricaoCurta,DescricaoDetalhada,EmEstoque,ImagemThumbnailUrl,ImagemUrl,IsLanchePreferido,Nome,Preco) select (SELECT CategoriaId FROM Categorias Where CategoriaNome='Normal'),'Pão, hambúrger, ovo, presunto, queijo e batata palha','Delicioso pão de hambúrger com ovo frito; presunto e queijo de primeira qualidade acompanhado com batata palha',1, 'http://www.macoratti.net/Imagens/lanches/cheesesalada1.jpg','http://www.macoratti.net/Imagens/lanches/cheesesalada1.jpg', 0 ,'Cheese Salada', 12.50");
            migrationBuilder.Sql("INSERT INTO Lanches(CategoriaId,DescricaoCurta,DescricaoDetalhada,EmEstoque,ImagemThumbnailUrl,ImagemUrl,IsLanchePreferido,Nome,Preco) select (SELECT CategoriaId FROM Categorias Where CategoriaNome='Normal'),'Pão, presunto, mussarela e tomate','Delicioso pão francês quentinho na chapa com presunto e mussarela bem servidos com tomate preparado com carinho.',1,'http://www.macoratti.net/Imagens/lanches/mistoquente4.jpg','http://www.macoratti.net/Imagens/lanches/mistoquente4.jpg',0,'Misto Quente', 8.00");
            migrationBuilder.Sql("INSERT INTO Lanches(CategoriaId,DescricaoCurta,DescricaoDetalhada,EmEstoque,ImagemThumbnailUrl,ImagemUrl,IsLanchePreferido,Nome,Preco) select (SELECT CategoriaId FROM Categorias Where CategoriaNome='Normal'),'Pão, hambúrger, presunto, mussarela e batalha palha','Pão de hambúrger especial com hambúrger de nossa preparação e presunto e mussarela; acompanha batata palha.',1,'http://www.macoratti.net/Imagens/lanches/cheeseburger1.jpg','http://www.macoratti.net/Imagens/lanches/cheeseburger1.jpg',0,'Cheese Burger', 11.00");
            migrationBuilder.Sql("INSERT INTO Lanches(CategoriaId,DescricaoCurta,DescricaoDetalhada,EmEstoque,ImagemThumbnailUrl,ImagemUrl,IsLanchePreferido,Nome,Preco) select (SELECT CategoriaId FROM Categorias Where CategoriaNome='Natural'),'Pão Integral, queijo branco, peito de peru, cenoura, alface, iogurte','Pão integral natural com queijo branco, peito de peru e cenoura ralada com alface picado e iogurte natural.',1,'http://www.macoratti.net/Imagens/lanches/lanchenatural.jpg','http://www.macoratti.net/Imagens/lanches/lanchenatural.jpg',1,'Peito Peru', 15.00");
            migrationBuilder.Sql("INSERT INTO Lanches(CategoriaId,DescricaoCurta,DescricaoDetalhada,EmEstoque,ImagemThumbnailUrl,ImagemUrl,IsLanchePreferido,Nome,Preco) select (SELECT CategoriaId FROM Categorias Where CategoriaNome='Natural'),'Pão Integral com pasta de atum','Pão integral natural com pasta de atum feita com iogurte.',1,'https://images.rappi.com.br/products/dfe23fc3-1026-4b77-a816-c229a50ccd4e-1588336152312.png','https://images.rappi.com.br/products/dfe23fc3-1026-4b77-a816-c229a50ccd4e-1588336152312.png',1,'Atum', 13.50");
            migrationBuilder.Sql("INSERT INTO Lanches(CategoriaId,DescricaoCurta,DescricaoDetalhada,EmEstoque,ImagemThumbnailUrl,ImagemUrl,IsLanchePreferido,Nome,Preco) select (SELECT CategoriaId FROM Categorias Where CategoriaNome='Bebidas'),'Milkshake de chocolate','Milkshake de chocolate 500 ml.',1,'https://photos.bigoven.com/recipe/hero/chocolate-milk-shakes.jpg','https://photos.bigoven.com/recipe/hero/chocolate-milk-shakes.jpg',0,'Milkshake', 11.00");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Categorias");
            migrationBuilder.Sql("DELETE FROM Lanches");
        }
    }
}
