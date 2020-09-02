using OnboardingSIGDB1.Domain.Funcionarios.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnboardingSIGDB1.Domain.Funcionarios.Interfaces.Validadores
{
    public interface IServicoDeValidacaoDeFuncionarios
    {
        void Executar(Funcionario funcionario);
    }
}
