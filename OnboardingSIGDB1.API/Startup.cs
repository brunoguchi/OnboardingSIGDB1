using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OnboardingSIGDB1.API.Extensions;
using OnboardingSIGDB1.API.Filters;
using OnboardingSIGDB1.API.Mappers;
using OnboardingSIGDB1.Data.Repositorio;
using OnboardingSIGDB1.IOC;

namespace OnboardingSIGDB1.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SIGDB1Context>(option =>
            {
                option.UseSqlServer(Configuration.GetConnectionString("ConexaoPadrao"));
            });

            services.AddMvc(opt =>
            {
                opt.Filters.Add<NotificationFilter>();
                opt.Filters.Add<CustomExceptionFilter>();
            })
                .AddFluentValidation(f => f.RegisterValidatorsFromAssemblyContaining<Startup>())
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddRepositorios();
            services.AddServicosDeDominio();
            services.AddAutoMapper();
            services.AddNucleo();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.AddCustomMiddleware();
            app.UseMvc();
        }
    }
}
