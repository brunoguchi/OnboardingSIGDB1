using Microsoft.Extensions.DependencyInjection;
using OnboardingSIGDB1.Domain.Cargos.Interfaces.Servicos;
using OnboardingSIGDB1.Domain.Empresas.Interfaces.Servicos;
using OnboardingSIGDB1.Domain.Empresas.Interfaces.Validadores;
using OnboardingSIGDB1.Domain.Funcionarios.Interfaces.Servicos;
using OnboardingSIGDB1.Domain.Funcionarios.Interfaces.Validadores;
using OnboardingSIGDB1.Domain.Servicos;

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
