using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain.Cargos.Interfaces.Servicos
{
    public interface IRemovedorDeCargos
    {
        Task Deletar(int id);
    }
}
