using FluentValidation;
using OnboardingSIGDB1.Core.Resources;
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
        public virtual List<FuncionarioCargo> FuncionariosCargos { get; set; }

        public void AtualizarDescricao(string descricao) => this.Descricao = descricao;

        public bool Validar()
        {
            return base.Validate(this, new CargoValidator());
        }

        public Cargo() { }

        public Cargo(string descricao)
        {
            this.Descricao = descricao;
        }

        public class CargoValidator : AbstractValidator<Cargo>
        {
            public CargoValidator()
            {
                RuleFor(x => x.Descricao)
                    .NotEmpty().WithMessage(string.Format(Mensagens.CampoObrigatorio, Mensagens.CampoDescricao))
                    .NotNull().WithMessage(string.Format(Mensagens.CampoObrigatorio, Mensagens.CampoDescricao))
                    .MaximumLength(250).WithMessage(string.Format(Mensagens.CampoComTamanhoMaximo, Mensagens.CampoDescricao, Mensagens.Tamanho250));
            }
        }
    }
}
