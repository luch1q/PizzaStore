﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using PizzaStore.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace PizzaStore
{
    public class Startup
    {
        public Startup(IConfiguration configuration) =>
            Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => 
                options.UseSqlServer(
                    Configuration["Data:PizzaStoreProducts:ConnectionString"]));
            
            services.AddTransient<IProductRepository, EFProductRepository>();
            services.AddTransient<IOrderRepository, EFOrderRepository>();
            services.AddMvc();
            services.AddMemoryCache();
            services.AddSession();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseSession();
            app.UseMvc(routes => {
                routes.MapRoute(
                name: null,
                    template: "{category}/Page{productPage:int}",
                    defaults: new { controller = "Product", action = "List"}
                    );
                routes.MapRoute(
                    name: null,
                    template: "Page{productPage:int}",
                    defaults: new
                    {
                        controller = "Product",
                        action = "List",
                        productPage = 1
                    });
                routes.MapRoute(
                    name: null,
                    template: "{category}",
                    defaults: new
                    {
                        controller = "Product",
                        action = "List",
                        productPage = 1
                    }
                );
                routes.MapRoute(
                    name: null,
                    template: "",
                    defaults: new
                    {
                        controller = "Product",
                        action = "List",
                        productPage = 1
                    });
                routes.MapRoute(name: null, template: "{controller}/{action}/{id?}");

            }
            );
            SeedData.EnsurePopulated(app);
        }
    }
}