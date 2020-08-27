using OnboardingSIGDB1.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnboardingSIGDB1.Interfaces.Domain
{
    public interface IServicoDeDominioDeFuncionarios
    {
        IEnumerable<Funcionario> ListarTodos();
        Funcionario RecuperarPorId(int id);
        IEnumerable<Funcionario> PesquisarPorFiltro(FuncionarioFiltro filtro);
        void Adicionar(Funcionario funcionario);
        void Atualizar(Funcionario funcionario);
        void Deletar(int id);
        void VincularFuncionarioAEmpresa(Funcionario funcionario);
    }
}
