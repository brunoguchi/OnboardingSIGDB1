using Microsoft.EntityFrameworkCore;
using OnboardingSIGDB1.Data.Repositorio;
using OnboardingSIGDB1.Domain.Funcionarios.Entidades;
using OnboardingSIGDB1.Domain.Funcionarios.Interfaces.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Data.Funcionarios.Repositorio
{
    public class RepositorioDeFuncionarios : RepositorioBase<Funcionario>, IRepositorioDeFuncionarios
    {
        private readonly SIGDB1Context _context;

        public RepositorioDeFuncionarios(SIGDB1Context context) : base(context)
        {
            _context = context;
        }

        public async Task<Funcionario> RecuperarPorIdComCargos(int id)
        {
            var consulta = _context.Funcionarios.Where(x => x.Id == id)
                            .Include(x => x.FuncionariosCargos)
                            .FirstOrDefault();

            return consulta;
        }
    }
}
