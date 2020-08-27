using Microsoft.Extensions.DependencyInjection;
using OnboardingSIGDB1.Domain.Servicos;
using OnboardingSIGDB1.Interfaces.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnboardingSIGDB1.IOC
{
    public static class DominioIOC
    {
        public static void AddServicosDeDominio(this IServiceCollection services)
        {
            services.AddScoped<IServicoDeDominioDeEmpresas, ServicoDeDominioDeEmpresas>();
            services.AddScoped<IServicoDeValidacaoDeEmpresas, ServicoDeValidacaoDeEmpresas>();
            services.AddScoped<IServicoDeDominioDeCargos, ServicoDeDominioDeCargos>();
            services.AddScoped<IServicoDeDominioDeFuncionarios, ServicoDeDominioDeFuncionarios>();
            services.AddScoped<IServicoDeValidacaoDeFuncionarios, ServicoDeValidacaoDeFuncionarios>();
        }
    }
}
