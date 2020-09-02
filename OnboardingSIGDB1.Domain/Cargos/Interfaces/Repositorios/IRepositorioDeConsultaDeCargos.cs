using OnboardingSIGDB1.Domain.Cargos.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnboardingSIGDB1.Domain.Cargos.Interfaces.Repositorios
{
    public interface IRepositorioDeConsultaDeCargos
    {
        IEnumerable<Cargo> ListarTodos();
        Cargo RecuperarPorId(int id);
    }
}
