using System;
using System.Collections.Generic;
using System.Text;

namespace OnboardingSIGDB1.Domain.Entities
{
    public class Funcionario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public DateTime DataContratacao { get; set; }
        public int EmpresaId { get; set; }
        public Empresa Empresa { get; set; }
        public List<FuncionarioCargo> FuncionariosCargos { get; set; }
    }
}
