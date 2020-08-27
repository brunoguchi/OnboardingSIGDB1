using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnboardingSIGDB1.Domain.Entities
{
    public class EmpresaFiltro : EntidadeValidacao
    {
        public string Nome { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public string Cnpj { get; set; }

        public void Validar()
        {
            base.Validate(this, new EmpresaFiltroValidator());
        }
    }

    public class EmpresaFiltroValidator : AbstractValidator<EmpresaFiltro>
    {
        public EmpresaFiltroValidator()
        {
           
        }
    }
}
