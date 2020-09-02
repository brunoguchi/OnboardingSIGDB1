using OnboardingSIGDB1.Domain.Empresas.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain.Empresas.Interfaces.Repositorios
{
    public interface IRepositorioDeConsultaDeEmpresas
    {
        IEnumerable<Empresa> ListarTodas();
        Empresa RecuperarPorId(int id);
        IEnumerable<Empresa> RecuperarPorFiltro(EmpresaFiltro filtro);
        Empresa RecuperarPorCnpj(string cnpj);
    }
}
