using OnboardingSIGDB1.Domain.Empresas.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain.Empresas.Interfaces.Servicos
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
