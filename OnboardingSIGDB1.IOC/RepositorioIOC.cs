using Microsoft.Extensions.DependencyInjection;
using OnboardingSIGDB1.Data.Cargos.Consultas;
using OnboardingSIGDB1.Data.Repositorio;
using OnboardingSIGDB1.Domain.Base.Interfaces;
using OnboardingSIGDB1.Domain.Cargos.Interfaces.Consultas;
using OnboardingSIGDB1.Domain.Empresas.Interfaces.Repositorios;
using OnboardingSIGDB1.Domain.Funcionarios.Interfaces.Repositorios;

namespace OnboardingSIGDB1.IOC
{
    public static class RepositorioIOC
    {
        public static void AddRepositorios(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepositorioBase<>), typeof(RepositorioBase<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IConsultasDeEmpresas, ConsultasDeEmpresas>();
            services.AddScoped<IConsultasDeCargos, ConsultasDeCargos>();
            services.AddScoped<IConsultaDeFuncionarios, ConsultaDeFuncionarios>();
        }
    }
}
