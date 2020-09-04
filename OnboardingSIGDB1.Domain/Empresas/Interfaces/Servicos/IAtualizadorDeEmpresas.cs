using OnboardingSIGDB1.Domain.Empresas.Dtos;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain.Empresas.Interfaces.Servicos
{
    public interface IAtualizadorDeEmpresas
    {
        Task Atualizar(EmpresaDto empresa);
    }
}
