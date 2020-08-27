using OnboardingSIGDB1.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Interfaces.Domain
{
    public interface IServicoDeDominioDeEmpresas
    {
        IEnumerable<Empresa> ListarTodas();
        Empresa RecuperarPorId(int id);
        IEnumerable<Empresa> PesquisarPorFiltro(EmpresaFiltro filtro);
        void Adicionar(Empresa empresa);
        void Atualizar(Empresa empresa);
        void Deletar(int id);
    }
}
