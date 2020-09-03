using AutoMapper;
using OnboardingSIGDB1.Domain.Empresas.Dtos;
using OnboardingSIGDB1.Domain.Empresas.Entidades;
using OnboardingSIGDB1.Domain.Empresas.Interfaces.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnboardingSIGDB1.Core.Extensions;

namespace OnboardingSIGDB1.Data.Repositorio
{
    public class ConsultasDeEmpresas : RepositorioBase<Empresa>, IConsultasDeEmpresas
    {
        private readonly SIGDB1Context _context;
        private readonly IMapper _iMapper;

        public ConsultasDeEmpresas(SIGDB1Context context,
            IMapper iMapper) : base(context)
        {
            _context = context;
            _iMapper = iMapper;
        }

        public async Task<IEnumerable<EmpresaDto>> RecuperarPorFiltro(EmpresaFiltroDto filtro)
        {
            var consulta = (from x in _context.Empresas
                            where (string.IsNullOrEmpty(filtro.Nome) || x.Nome.Contains(filtro.Nome))
                               && (string.IsNullOrEmpty(filtro.Cnpj.RemoverFormatacaoDocumento()) || x.Cnpj.Equals(filtro.Cnpj.RemoverFormatacaoDocumento()))
                               && ((filtro.DataInicio == DateTime.MinValue || filtro.DataFim == DateTime.MinValue) ||
                                  (x.DataFundacao.Date >= filtro.DataInicio && x.DataFundacao.Date <= filtro.DataFim))
                            select x).ToList();

            return _iMapper.Map<List<EmpresaDto>>(consulta);
        }

        public async Task<IEnumerable<EmpresaDto>> ListarTodas()
        {
            var consulta = await GetAll();

            return _iMapper.Map<List<EmpresaDto>>(consulta);
        }

        public async Task<EmpresaDto> RecuperarPorCnpj(string cnpj)
        {
            var consulta = _context.Empresas.Where(x => x.Cnpj.Equals(cnpj)).FirstOrDefault();

            return _iMapper.Map<EmpresaDto>(consulta);
        }

        public async Task<EmpresaDto> RecuperarPorId(int id)
        {
            var consulta = await GetById(id);

            return _iMapper.Map<EmpresaDto>(consulta);
        }
    }
}
