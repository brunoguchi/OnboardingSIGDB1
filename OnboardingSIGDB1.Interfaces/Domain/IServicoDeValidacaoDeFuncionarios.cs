using OnboardingSIGDB1.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnboardingSIGDB1.Interfaces.Domain
{
    public interface IServicoDeValidacaoDeFuncionarios
    {
        void Executar(Funcionario funcionario);
        void ValidarCpf(string cpf);
        void ValidarSeFuncionarioExistentePorDocumento(string cpf);
    }
}
