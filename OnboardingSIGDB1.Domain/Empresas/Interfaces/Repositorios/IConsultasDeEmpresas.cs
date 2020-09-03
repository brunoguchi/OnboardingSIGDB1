using OnboardingSIGDB1.Domain.Empresas.Dtos;
using OnboardingSIGDB1.Domain.Empresas.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain.Empresas.Interfaces.Repositorios
{
    public interface IConsultasDeEmpresas
    {
        Task<IEnumerable<EmpresaDto>> RecuperarPorFiltro(EmpresaFiltroDto filtro);
        Task<EmpresaDto> RecuperarPorCnpj(string cnpj);

        Task<IEnumerable<EmpresaDto>> ListarTodas();
        Task<EmpresaDto> RecuperarPorId(int id);
    }
}
