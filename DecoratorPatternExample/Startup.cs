using System.Collections.Generic;
using DecoratorPatternExample.DataAccessLayer;
using DecoratorPatternExample.DataAccessLayer.Imp;
using DecoratorPatternExample.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace DecoratorPatternExample
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "DecoratorPatternExample", Version = "v1"}); });

            services.AddMemoryCache();
            services.AddDbContext<ProductDbContext>(builder => builder.UseInMemoryDatabase("ProductDb"));

            services.AddScoped<IProductRepository, DbProductRepository>();
            services.Decorate<IProductRepository>((inner, provider) => new CacheProductRepository(inner, provider.GetRequiredService<IMemoryCache>(), provider.GetRequiredService<ILoggerFactory>().CreateLogger<CacheProductRepository>()));

            services.AddScoped<IAdminProductRepository>(provider => new AdminProductRepository(provider.GetRequiredService<IProductRepository>(),
                                                                                               provider.GetRequiredService<ProductDbContext>())
                                                       );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ProductDbContext productDbContext)
        {
            productDbContext.Database.EnsureCreated();
            productDbContext.Products.AddRange(new List<Product>()
                                               {
                                                   new Product("product-1", 1.99m),
                                                   new Product("product-2", 2.99m),
                                                   new Product("product-3", 3.49m),
                                                   new Product("product-4", 4.59m)
                                               });
            productDbContext.SaveChanges();


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DecoratorPatternExample v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}