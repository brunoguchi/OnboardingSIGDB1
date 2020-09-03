using FluentValidation;
using OnboardingSIGDB1.Domain.Base.Entidades;
using OnboardingSIGDB1.Domain.Empresas.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnboardingSIGDB1.Domain.Funcionarios.Entidades
{
    public class Funcionario : EntidadeValidacao
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public DateTime DataContratacao { get; set; }
        public int? EmpresaId { get; set; }
        public Empresa Empresa { get; set; }
        public List<FuncionarioCargo> FuncionariosCargos { get; set; }

        public void Validar()
        {
            base.Validate(this, new FuncionarioValidator());
        }

        public class FuncionarioValidator : AbstractValidator<Funcionario>
        {
            public FuncionarioValidator()
            {
                RuleFor(x => x.Nome)
                    .NotEmpty().WithMessage("Nome é obrigatório ser preenchido")
                    .MaximumLength(150).WithMessage("Tamanho do nome não deve ultrapassar 150 caracteres");

                RuleFor(x => x.Cpf)
                    .NotEmpty()
                    .NotNull()
                    .MaximumLength(11)
                    .Length(11);

                RuleFor(x => x.DataContratacao)
                    .NotEmpty()
                    .Must(x => x > DateTime.MinValue);
            }
        }
    }
}
