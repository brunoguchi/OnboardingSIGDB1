using OnboardingSIGDB1.Domain.Funcionarios.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnboardingSIGDB1.Domain.Funcionarios.Interfaces.Repositorios
{
    public interface IRepositorioDeConsultaDeFuncionarios
    {
        IEnumerable<Funcionario> ListarTodos();
        Funcionario RecuperarPorId(int id);
        IEnumerable<Funcionario> RecuperarPorFiltro(FuncionarioFiltro filtro);
        Funcionario RecuperarPorCpf(string cnpj);
        Funcionario RecuperarPorIdComTodosOsCargos(int id);
    }
}
