using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain.Empresas.Dtos
{
    public class EmpresaDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cnpj { get; set; }
        public DateTime? DataFundacao { get; set; }
    }
}
