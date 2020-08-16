using LanchesMC.Areas.Admin.Servicos;
using LanchesMC.Context;
using LanchesMC.Models;
using LanchesMC.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ReflectionIT.Mvc.Paging;

namespace LanchesMC
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // por causa da autenticação - sistema de identity padrão:
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            // acesso negado a area de admin
            services.ConfigureApplicationCookie(options => options.AccessDeniedPath = "/Home/AccessDenied");

            services.AddTransient<ICategoriaRepository, CategoriaRepository>(); // objeto do servico sera criado em toda requisicao
            services.AddTransient<ILancheRepository, LancheRepository>();
            services.AddTransient<IPedidoRepository, PedidoRepository>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); // objeto sera criado para todas as reqs - mesmo objeto - para poder ter acesso a sessao

            // para o relatorio de vendas por periodo
            services.AddScoped<RelatorioVendasService>();

            services.AddScoped(cp => CarrinhoCompra.GetCarrinho(cp)); // criando objeto para cada requisicao - se duas pessoas solicitarem ao mesmo tempo, serao instancias diferentes

            services.AddControllersWithViews();

            // configuracao do serviço de paginação (lanches e pedidos)
            services.AddPaging(options =>
            {
                options.ViewName = "Bootstrap4";
                options.PageParameterName = "pageindex";
            });

            // para sessoes funcionarem
            services.AddMemoryCache();
            services.AddSession();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseSession();       // ativar o uso das sessions - middleware

            app.UseAuthentication();    // habilitando a autenticação pelo middleware... adiciona um único componente de autenticação ao pipeline da solicitação

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "AdminArea",
                    pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}"); // areas tem que vir primeiro...



                endpoints.MapControllerRoute(
                    name: "filtrarPorCategoria",
                    pattern: "Lanche/{action}/{categoria}",
                    defaults: new { Controller = "Lanche", action = "List" }
                    );
                // a ordem q coloca aqui é importante

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                //app.UseMvcWithDefaultRoute(); // mesma coisa do q acima

            });



        }
    }
}
