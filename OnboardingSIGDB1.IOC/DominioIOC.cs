using Microsoft.Extensions.DependencyInjection;
using OnboardingSIGDB1.Domain.Cargos.Interfaces.Servicos;
using OnboardingSIGDB1.Domain.Cargos.Servicos;
using OnboardingSIGDB1.Domain.Empresas.Interfaces.Servicos;
using OnboardingSIGDB1.Domain.Empresas.Interfaces.Validadores;
using OnboardingSIGDB1.Domain.Empresas.Servicos;
using OnboardingSIGDB1.Domain.Funcionarios.Interfaces.Servicos;
using OnboardingSIGDB1.Domain.Funcionarios.Interfaces.Validadores;
using OnboardingSIGDB1.Domain.Funcionarios.Servicos;
using OnboardingSIGDB1.Domain.Servicos;

namespace OnboardingSIGDB1.IOC
{
    public static class DominioIOC
    {
        public static void AddServicosDeDominio(this IServiceCollection services)
        {
            services.AddScoped<IArmazenadorDeCargos, ArmazenadorDeCargos>();
            services.AddScoped<IAtualizadorDeCargos, AtualizadorDeCargos>();
            services.AddScoped<IRemovedorDeCargos, RemovedorDeCargos>();

            services.AddScoped<IArmazenadorDeEmpresas, ArmazenadorDeEmpresas>();
            services.AddScoped<IAtualizadorDeEmpresas, AtualizadorDeEmpresas>();
            services.AddScoped<IRemovedorDeEmpresas, RemovedorDeEmpresas>();
            services.AddScoped<IServicoDeValidacaoDeEmpresas, ServicoDeValidacaoDeEmpresas>();

            services.AddScoped<IArmazenadorDeFuncionarios, ArmazenadorDeFuncionarios>();
            services.AddScoped<IAtualizadorDeFuncionarios, AtualizadorDeFuncionarios>();
            services.AddScoped<IRemovedorDeFuncionarios, RemovedorDeFuncionarios>();
            services.AddScoped<IServicoDeValidacaoDeFuncionarios, ServicoDeValidacaoDeFuncionarios>();
            services.AddScoped<IVinculadorDeFuncionarioCargo, VinculadorDeFuncionarioCargo>();
            services.AddScoped<IVinculadorDeFuncionarioEmpresa, VinculadorDeFuncionarioEmpresa>();
        }
    }
}
