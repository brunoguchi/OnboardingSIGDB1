using OnboardingSIGDB1.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnboardingSIGDB1.Interfaces.Data
{
    public interface IRepositorioDeConsultaDeFuncionarios
    {
        IEnumerable<Funcionario> ListarTodos();
        Funcionario RecuperarPorId(int id);
        IEnumerable<Funcionario> RecuperarPorFiltro(FuncionarioFiltro filtro);
        Funcionario RecuperarPorCpf(string cnpj);
    }
}
