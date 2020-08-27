using OnboardingSIGDB1.Domain.Entities;
using OnboardingSIGDB1.Interfaces.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnboardingSIGDB1.Data.Repositorio
{
    public class RepositorioDeConsultaDeCargos : IRepositorioDeConsultaDeCargos
    {
        public IEnumerable<Cargo> ListarTodos()
        {
            throw new NotImplementedException();
        }

        public Cargo RecuperarPorId(int id)
        {
            throw new NotImplementedException();
        }
    }
}
