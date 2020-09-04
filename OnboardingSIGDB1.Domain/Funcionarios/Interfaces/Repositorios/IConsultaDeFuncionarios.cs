using OnboardingSIGDB1.Domain.Funcionarios.Dtos;
using OnboardingSIGDB1.Domain.Funcionarios.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain.Funcionarios.Interfaces.Repositorios
{
    public interface IConsultaDeFuncionarios
    {
        Task<IEnumerable<FuncionarioDto>> ListarTodos();
        Task<IEnumerable<FuncionarioDto>> RecuperarPorFiltro(FuncionarioFiltroDto filtro);
        Task<FuncionarioDto> RecuperarPorCpf(string cpf);
        Task<FuncionarioDto> RecuperarPorIdComTodosOsCargos(int id);
        Task<FuncionarioDto> RecuperarPorIdComUltimoCargo(int id);
    }
}
