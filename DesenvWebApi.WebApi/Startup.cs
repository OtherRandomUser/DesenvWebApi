using DesenvWebApi.Data;
using DesenvWebApi.Domain;
using DesenvWebApi.Domain.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;

namespace DesenvWebApi.WebApi
{
    public class Startup
    {
        private IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(op => op
                .UseMySql(_config.GetConnectionString("db")));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IRepository<Curriculum>, GenericRepository<Curriculum>>();
            services.AddScoped<IRepository<Subject>, GenericRepository<Subject>>();
            services.AddScoped<IRepository<SubjectCurriculum>, GenericRepository<SubjectCurriculum>>();
            services.AddScoped<IRepository<User>, GenericRepository<User>>();

            services.AddMvc(op =>
            {
                op.EnableEndpointRouting = false;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Desenv Api",
                    Version = "v1"
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("swagger/v1/swagger.json", "Soapstone Backend v1");
                c.RoutePrefix = string.Empty;
            });

            app.UseCors((e) => {
                e.AllowAnyHeader();
                e.AllowAnyMethod();
                e.AllowAnyOrigin();
            });

            app.UseMvc();
        }
    }
}
