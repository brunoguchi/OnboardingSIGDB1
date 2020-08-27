using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnboardingSIGDB1.Domain.Entities
{
    public class Funcionario : EntidadeValidacao
    {
        public int Id { get; set; }
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
                    .MaximumLength(11)
                    .Length(11);

                RuleFor(x => x.DataContratacao)
                    .NotEmpty()
                    .Must(x => x > DateTime.MinValue);
            }
        }
    }
}
