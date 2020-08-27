using OnboardingSIGDB1.Domain.Entities;
using OnboardingSIGDB1.Interfaces.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnboardingSIGDB1.Data.Repositorio
{
    public class RepositorioDeConsultaDeCargos : IRepositorioDeConsultaDeCargos
    {
        private readonly SIGDB1Context _context;

        public RepositorioDeConsultaDeCargos(SIGDB1Context context)
        {
            _context = context;
        }

        public IEnumerable<Cargo> ListarTodos()
        {
            return _context.Cargos.ToList();
        }

        public Cargo RecuperarPorId(int id)
        {
            return _context.Cargos.Where(x => x.Id == id).FirstOrDefault();
        }
    }
}
