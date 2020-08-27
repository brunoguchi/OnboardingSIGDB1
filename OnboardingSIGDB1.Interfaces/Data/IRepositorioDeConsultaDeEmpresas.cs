using OnboardingSIGDB1.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Interfaces.Data
{
    public interface IRepositorioDeConsultaDeEmpresas
    {
        IEnumerable<Empresa> ListarTodas();
        Empresa RecuperarPorId(int id);
        IEnumerable<Empresa> RecuperarPorFiltro(EmpresaFiltro filtro);
        Empresa RecuperarPorCnpj(string cnpj);
    }
}
