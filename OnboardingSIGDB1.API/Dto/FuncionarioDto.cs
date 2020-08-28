using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.API.Dto
{
    public class FuncionarioDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public DateTime DataContratacao { get; set; }
        public int? EmpresaId { get; set; }
        public List<FuncionarioCargoDto> FuncionariosCargos { get; set; }
    }
}
