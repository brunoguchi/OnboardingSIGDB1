﻿using Microsoft.Extensions.DependencyInjection;
using OnboardingSIGDB1.Data.Repositorio;
using OnboardingSIGDB1.Domain.Base.Interfaces;
using OnboardingSIGDB1.Domain.Cargos.Interfaces.Repositorios;
using OnboardingSIGDB1.Domain.Empresas.Interfaces.Repositorios;
using OnboardingSIGDB1.Domain.Funcionarios.Interfaces.Repositorios;

namespace OnboardingSIGDB1.IOC
{
    public static class RepositorioIOC
    {
        public static void AddRepositorios(this IServiceCollection services)
        {
            services.AddScoped<IRepositorioBase, RepositorioBase>();
            services.AddScoped<IRepositorioDeConsultaDeEmpresas, RepositorioDeConsultaDeEmpresas>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IRepositorioDeConsultaDeCargos, RepositorioDeConsultaDeCargos>();
            services.AddScoped<IRepositorioDeConsultaDeFuncionarios, RepositorioDeConsultaDeFuncionarios>();
        }
    }
}
