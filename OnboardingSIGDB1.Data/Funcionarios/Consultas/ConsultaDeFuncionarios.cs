using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnboardingSIGDB1.Core.Extensions;
using OnboardingSIGDB1.Domain.Funcionarios.Dtos;
using OnboardingSIGDB1.Domain.Funcionarios.Entidades;
using OnboardingSIGDB1.Domain.Funcionarios.Interfaces.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Data.Repositorio
{
    public class ConsultaDeFuncionarios : RepositorioBase<Funcionario>, IConsultaDeFuncionarios
    {
        private readonly SIGDB1Context _context;
        private readonly IMapper _iMapper;

        public ConsultaDeFuncionarios(SIGDB1Context context,
            IMapper iMapper) : base(context)
        {
            _context = context;
            _iMapper = iMapper;
        }

        public async Task<IEnumerable<FuncionarioDto>> ListarTodos()
        {
            var consulta = await GetAll();

            return _iMapper.Map<List<FuncionarioDto>>(consulta);
        }

        public async Task<FuncionarioDto> RecuperarPorCpf(string cpf)
        {
            var consulta = _context.Funcionarios.Where(x => x.Cpf.Equals(cpf)).FirstOrDefault();

            return _iMapper.Map<FuncionarioDto>(consulta);
        }

        public async Task<IEnumerable<FuncionarioDto>> RecuperarPorFiltro(FuncionarioFiltroDto filtro)
        {
            var consulta = (from x in _context.Funcionarios
                            where (string.IsNullOrEmpty(filtro.Nome) || x.Nome.Contains(filtro.Nome))
                               && (string.IsNullOrEmpty(filtro.Cpf.RemoverFormatacaoDocumento()) || x.Cpf.Equals(filtro.Cpf.RemoverFormatacaoDocumento()))
                               && ((filtro.DataInicio == DateTime.MinValue || filtro.DataFim == DateTime.MinValue) ||
                                  (x.DataContratacao.Date >= filtro.DataInicio && x.DataContratacao.Date <= filtro.DataFim))
                            select x).ToList();

            return _iMapper.Map<List<FuncionarioDto>>(consulta);
        }

        public async Task<FuncionarioDto> RecuperarPorIdComTodosOsCargos(int id)
        {
            var consulta = _context.Funcionarios.Where(x => x.Id == id).AsNoTracking().Include(x => x.FuncionariosCargos).FirstOrDefault();

            return _iMapper.Map<FuncionarioDto>(consulta);
        }

        public async Task<FuncionarioDto> RecuperarPorIdComUltimoCargo(int id)
        {
            var consulta = _context.Funcionarios.Where(x => x.Id == id)
                            .AsNoTracking()
                            .Include(x => x.FuncionariosCargos)
                            .Select(x => new FuncionarioDto
                            {
                                Id = x.Id,
                                Cpf = x.Cpf,
                                DataContratacao = x.DataContratacao,
                                EmpresaId = x.EmpresaId,
                                Nome = x.Nome,
                                //FuncionariosCargos = _iMapper.Map<List<FuncionarioCargoDto>>(x.FuncionariosCargos.OrderByDescending(w => w.DataDeVinculo).FirstOrDefault())
                            })
                            .FirstOrDefault();

            return _iMapper.Map<FuncionarioDto>(consulta);
        }
    }
}
