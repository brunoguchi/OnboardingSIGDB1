using FluentValidation;
using OnboardingSIGDB1.Domain.Base.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnboardingSIGDB1.Domain.Empresas.Entidades
{
    public class Empresa : EntidadeValidacao
    {
        public string Nome { get; set; }
        public string Cnpj { get; set; }
        public DateTime DataFundacao { get; set; }

        public void Validar()
        {
            base.Validate(this, new EmpresaValidator());
        }

        public class EmpresaValidator : AbstractValidator<Empresa>
        {
            public EmpresaValidator()
            {
                RuleFor(x => x.Nome)
                    .NotEmpty().WithMessage("Nome é obrigatório ser preenchido")
                    .MaximumLength(150).WithMessage("Tamanho do nome não deve ultrapassar 150 caracteres");

                RuleFor(x => x.Cnpj)
                    .NotEmpty()
                    .MaximumLength(14)
                    .Length(14);

                RuleFor(x => x.DataFundacao)
                    .NotEmpty()
                    .Must(x => x > DateTime.MinValue);
            }
        }
    }
}
