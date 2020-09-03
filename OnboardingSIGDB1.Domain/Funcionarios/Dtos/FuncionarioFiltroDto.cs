using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain.Funcionarios.Dtos
{
    public class FuncionarioFiltroDto
    {
        public string Nome { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public string Cpf { get; set; }
    }
}
