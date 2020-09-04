using OnboardingSIGDB1.Domain.Funcionarios.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain.Funcionarios.Interfaces.Repositorios
{
    public interface IRepositorioDeFuncionarios
    {
        Task<Funcionario> RecuperarPorIdComCargos(int id);
    }
}
