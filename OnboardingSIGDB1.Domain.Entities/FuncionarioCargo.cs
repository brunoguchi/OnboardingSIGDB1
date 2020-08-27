using System;
using System.Collections.Generic;
using System.Text;

namespace OnboardingSIGDB1.Domain.Entities
{
    public class FuncionarioCargo
    {
        public int FuncionarioId { get; set; }
        public Funcionario Funcionario { get; set; }

        public int CargoId { get; set; }
        public Cargo Cargo { get; set; }

        public DateTime DataDeVinculo { get; set; }
    }
}
