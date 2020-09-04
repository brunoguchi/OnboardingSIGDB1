using OnboardingSIGDB1.Domain.Empresas.Dtos;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain.Empresas.Interfaces.Servicos
{
    public interface IArmazenadorDeEmpresas
    {
        Task Adicionar(EmpresaDto empresa);
    }
}
