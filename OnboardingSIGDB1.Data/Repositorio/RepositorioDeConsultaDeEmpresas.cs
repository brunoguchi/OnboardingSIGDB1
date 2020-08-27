using OnboardingSIGDB1.Domain.Entities;
using OnboardingSIGDB1.Interfaces.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Data.Repositorio
{
    public class RepositorioDeConsultaDeEmpresas : IRepositorioDeConsultaDeEmpresas
    {
        private readonly SIGDB1Context _context;

        public RepositorioDeConsultaDeEmpresas(SIGDB1Context context)
        {
            _context = context;
        }

        public IEnumerable<Empresa> ListarTodas()
        {
            var dados = _context.Empresas.ToList();
            return dados;
        }

        public Empresa RecuperarPorCnpj(string cnpj)
        {
            var dados = _context.Empresas.Where(x => x.Cnpj.Equals(cnpj)).FirstOrDefault();
            return dados;
        }

        public IEnumerable<Empresa> RecuperarPorFiltro(EmpresaFiltro filtro)
        {
            var dados = (from x in _context.Empresas
                         where (string.IsNullOrEmpty(filtro.Nome) || x.Nome.Contains(filtro.Nome))
                            && (string.IsNullOrEmpty(filtro.Cnpj) || x.Cnpj.Equals(filtro.Cnpj))
                            && ((filtro.DataInicio == DateTime.MinValue || filtro.DataFim == DateTime.MinValue) || 
                               (x.DataFundacao.Date >= filtro.DataInicio && x.DataFundacao.Date <= filtro.DataFim))
                         select x).ToList();

            return dados;
        }

        public Empresa RecuperarPorId(int id)
        {
            var dados = _context.Empresas.Where(x => x.Id == id).FirstOrDefault();
            return dados;
        }
    }
}
