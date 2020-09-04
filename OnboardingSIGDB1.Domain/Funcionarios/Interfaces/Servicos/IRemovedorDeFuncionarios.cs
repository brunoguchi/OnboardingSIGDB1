using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain.Funcionarios.Interfaces.Servicos
{
    public interface IRemovedorDeFuncionarios
    {
        Task Deletar(int id);
    }
}
