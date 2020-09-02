using OnboardingSIGDB1.Domain.Funcionarios.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnboardingSIGDB1.Domain.Funcionarios.Interfaces.Servicos
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
        void VincularFuncionarioAoCargo(FuncionarioCargo funcionarioCargo);
    }
}
