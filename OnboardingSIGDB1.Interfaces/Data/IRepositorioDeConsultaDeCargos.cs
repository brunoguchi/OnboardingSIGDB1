using OnboardingSIGDB1.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnboardingSIGDB1.Interfaces.Data
{
    public interface IRepositorioDeConsultaDeCargos
    {
        IEnumerable<Cargo> ListarTodos();
        Cargo RecuperarPorId(int id);
    }
}
