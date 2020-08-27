using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnboardingSIGDB1.Domain.Entities
{
    public class Cargo : EntidadeValidacao
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public List<FuncionarioCargo> FuncionariosCargos { get; set; }

        public void Validar()
        {
            base.Validate(this, new CargoValidator());
        }

        public class CargoValidator : AbstractValidator<Cargo>
        {
            public CargoValidator()
            {
                RuleFor(x => x.Descricao)
                    .NotEmpty().WithMessage("Descrição é obrigatório ser preenchido")
                    .MaximumLength(250).WithMessage("Tamanho da descriçao não deve ultrapassar 250 caracteres");
            }
        }
    }
}
