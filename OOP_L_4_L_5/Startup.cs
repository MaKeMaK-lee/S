using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OOP_L_4_L_5.Managers.Animes;
using OOP_L_4_L_5.Managers.Books;
using OOP_L_4_L_5.Managers.Clients;
using OOP_L_4_L_5.Managers.Figures;
using OOP_L_4_L_5.Managers.Orders;
using OOP_L_4_L_5.Managers.PaymentInfos;
using OOP_L_4_L_5.Managers.Products;
using OOP_L_4_L_5.Storage;

namespace OOP_L_4_L_5
{
    public class Startup
    {
        private IConfigurationRoot _configurationRoot;
        public Startup(Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _configurationRoot = new ConfigurationBuilder().SetBasePath(hostingEnvironment.ContentRootPath).AddJsonFile("RestShopDbSettings.json").Build();
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<RestShopDataContext>(options => options.UseSqlServer(_configurationRoot.GetConnectionString("RestShopDb")));
            services.AddMvc(option => option.EnableEndpointRouting = false);
            services.AddTransient<IAnimeManager, AnimeManager>();
            services.AddTransient<IBookManager, BookManager>();
            services.AddTransient<IFigureManager, FigureManager>();
            services.AddTransient<IPaymentInfoManager, PaymentInfoManager>();
            services.AddTransient<IClientManager, ClientManager>();
            services.AddTransient<IOrderManager, OrderManager>();
            services.AddTransient<IProductManager, ProductManager>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseRequestLocalization();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Anime}/{action=AnimeCatalog}/");

            });
        }
    }
}
