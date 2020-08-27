using Microsoft.Extensions.DependencyInjection;
using OnboardingSIGDB1.Data.Repositorio;
using OnboardingSIGDB1.Interfaces.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnboardingSIGDB1.IOC
{
    public static class RepositorioIOC
    {
        public static void AddRepositorios(this IServiceCollection services)
        {
            services.AddScoped<IRepositorioSIGDB1, RepositorioSIGDB1>();
            services.AddScoped<IRepositorioDeConsultaDeEmpresas, RepositorioDeConsultaDeEmpresas>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IRepositorioDeConsultaDeCargos, RepositorioDeConsultaDeCargos>();
            services.AddScoped<IRepositorioDeConsultaDeFuncionarios, RepositorioDeConsultaDeFuncionarios>();
        }
    }
}
