using OnboardingSIGDB1.Domain.Funcionarios.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain.Funcionarios.Interfaces.Validadores
{
    public interface IServicoDeValidacaoDeFuncionarios
    {
        Task Executar(Funcionario funcionario);
    }
}
