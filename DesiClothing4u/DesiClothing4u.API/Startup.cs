using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesiClothing4u.Common.Interfaces;
using DesiClothing4u.Common.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
//using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Cors;
using Newtonsoft.Json.Serialization;


namespace DesiClothing4u.API
{
    public class Startup
    {
        //readonly string CorsApi = "_myAllowSpecificOrigins";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddScoped<ICustomer, CustomerServer>();
            //services.AddControllers().Add
            services.AddDbContext<desiclothingContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DesiClothing4uDatabase")));
            services.AddCors(options =>
            {
                options.AddPolicy("CorsApi", builder => builder.WithOrigins("https://localhost:44328")
                .AllowAnyHeader()
                .AllowAnyMethod()
                /*options.AddPolicy("CorsApi", builder => builder.WithOrigins("https://test.desiclothingonline.com")
                .AllowAnyHeader()
                .AllowAnyMethod()*/
                );
            });
            services.AddMvc().AddControllersAsServices();
            //services.AddMvc()
            //.AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
               
            });
            services.AddControllers();
            //services.AddRazorPages(); //Added on Nov 29, 2020 to fix delete pic issue
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
           
            app.UseSwaggerUI(c =>
            {

                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Version 1");
                //c.RoutePrefix = "docs";

            });
            
            //if (env.IsDevelopment())
            //{
                app.UseDeveloperExceptionPage();
            //}

            app.UseHttpsRedirection();
            app.UseStaticFiles();//added on Oct 24 by SM
            app.UseRouting();
            app.UseCors("CorsApi");
           //app.UseCors(options => options.AllowAnyOrigin());
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapGet("/echo",
                //    context => context.Response.WriteAsync("echo"))
                //    .RequireCors("CorsApi");
                endpoints.MapControllers()
                .RequireCors("CorsApi");
            });
           
        }
    }
}
