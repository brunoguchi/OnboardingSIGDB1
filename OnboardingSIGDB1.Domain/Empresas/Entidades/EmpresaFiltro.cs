using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnboardingSIGDB1.Domain.Empresas.Entidades
{
    public class EmpresaFiltro
    {
        public string Nome { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public string Cnpj { get; set; }
    }
}
