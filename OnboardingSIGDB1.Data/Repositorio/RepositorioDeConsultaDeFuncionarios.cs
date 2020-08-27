using Microsoft.EntityFrameworkCore;
using OnboardingSIGDB1.Domain.Entities;
using OnboardingSIGDB1.Interfaces.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnboardingSIGDB1.Data.Repositorio
{
    public class RepositorioDeConsultaDeFuncionarios : IRepositorioDeConsultaDeFuncionarios
    {
        private readonly SIGDB1Context _context;

        public RepositorioDeConsultaDeFuncionarios(SIGDB1Context context)
        {
            _context = context;
        }

        public IEnumerable<Funcionario> ListarTodos()
        {
            var dados = _context.Funcionarios.ToList();
            return dados;
        }

        public Funcionario RecuperarPorCpf(string cpf)
        {
            var dados = _context.Funcionarios.Where(x => x.Cpf.Equals(cpf)).FirstOrDefault();
            return dados;
        }

        public IEnumerable<Funcionario> RecuperarPorFiltro(FuncionarioFiltro filtro)
        {
            var dados = (from x in _context.Funcionarios
                         where (string.IsNullOrEmpty(filtro.Nome) || x.Nome.Contains(filtro.Nome))
                            && (string.IsNullOrEmpty(filtro.Cpf) || x.Cpf.Equals(filtro.Cpf))
                            && ((filtro.DataInicio == DateTime.MinValue || filtro.DataFim == DateTime.MinValue) ||
                               (x.DataContratacao.Date >= filtro.DataInicio && x.DataContratacao.Date <= filtro.DataFim))
                         select x).ToList();

            return dados;
        }

        public Funcionario RecuperarPorId(int id)
        {
            var dados = _context.Funcionarios.Where(x => x.Id == id).AsNoTracking().FirstOrDefault();
            return dados;
        }
    }
}
