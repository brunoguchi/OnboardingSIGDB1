using FluentValidation;
using OnboardingSIGDB1.Domain.Base.Entidades;
using OnboardingSIGDB1.Domain.Base.Interfaces;
using OnboardingSIGDB1.Domain.Funcionarios.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain.Cargos.Entidades
{
    public class Cargo : EntidadeValidacao
    {
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
