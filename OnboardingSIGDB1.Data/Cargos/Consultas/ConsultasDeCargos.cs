using AutoMapper;
using OnboardingSIGDB1.Data.Repositorio;
using OnboardingSIGDB1.Domain.Cargos.Dtos;
using OnboardingSIGDB1.Domain.Cargos.Entidades;
using OnboardingSIGDB1.Domain.Cargos.Interfaces.Consultas;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Data.Cargos.Consultas
{
    public class ConsultasDeCargos : RepositorioBase<Cargo>, IConsultasDeCargos
    {
        private readonly SIGDB1Context _context;
        private readonly IMapper _iMapper;

        public ConsultasDeCargos(SIGDB1Context context,
                                 IMapper iMapper) : base(context)
        {
            _context = context;
            _iMapper = iMapper;
        }

        public async Task<IEnumerable<CargoDto>> ListarTodos()
        {
            var consulta = await GetAll();
          
            return _iMapper.Map<List<CargoDto>>(consulta);
        }

        public async Task<CargoDto> RecuperarPorId(int id)
        {
            var consulta = await GetById(id);
            
            return _iMapper.Map<CargoDto>(consulta);
        }
    }
}
