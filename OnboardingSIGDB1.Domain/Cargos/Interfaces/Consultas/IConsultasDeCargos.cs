using OnboardingSIGDB1.Domain.Cargos.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain.Cargos.Interfaces.Consultas
{
    public interface IConsultasDeCargos
    {
        Task<IEnumerable<CargoDto>> ListarTodos();
        Task<CargoDto> RecuperarPorId(int id);
    }
}
