using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain.Empresas.Interfaces.Servicos
{
    public interface IRemovedorDeEmpresas
    {
        Task Deletar(int id);
    }
}
