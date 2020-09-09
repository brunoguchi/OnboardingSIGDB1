using FluentValidation;
using OnboardingSIGDB1.Core.Extensions;
using OnboardingSIGDB1.Core.Resources;
using OnboardingSIGDB1.Domain.Base.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnboardingSIGDB1.Domain.Empresas.Entidades
{
    public class Empresa : EntidadeValidacao
    {
        public string Nome { get; private set; }
        public string Cnpj { get; private set; }
        public DateTime DataFundacao { get; private set; }

        public void AtualizarNome(string nome) => this.Nome = nome;
        public void AtualizarCnpj(string cnpj) => this.Cnpj = cnpj.RemoverFormatacaoDocumento();
        public void AtualizarDataFundacao(DateTime data) => this.DataFundacao = data;

        public bool Validar()
        {
            return base.Validate(this, new EmpresaValidator());
        }

        public Empresa() { }

        public Empresa(string nome, string cnpj, DateTime dataFundacao)
        {
            this.Nome = nome;
            this.Cnpj = cnpj;
            this.DataFundacao = dataFundacao;
        }

        public class EmpresaValidator : AbstractValidator<Empresa>
        {
            public EmpresaValidator()
            {
                RuleFor(x => x.Nome)
                    .NotEmpty().WithMessage(string.Format(Mensagens.CampoObrigatorio, Mensagens.CampoNome))
                    .MaximumLength(150).WithMessage(string.Format(Mensagens.CampoComTamanhoMaximo, Mensagens.CampoNome, Mensagens.Tamanho150));

                RuleFor(x => x.Cnpj)
                    .NotEmpty().WithMessage(string.Format(Mensagens.CampoObrigatorio, Mensagens.CampoCNPJ))
                    .NotNull().WithMessage(string.Format(Mensagens.CampoObrigatorio, Mensagens.CampoCNPJ))
                    .MaximumLength(14).WithMessage(string.Format(Mensagens.CampoComTamanhoMaximo, Mensagens.CampoCNPJ, Mensagens.Tamanho14))
                    .Length(14).WithMessage(string.Format(Mensagens.CampoComTamanhoFixo, Mensagens.CampoCNPJ, Mensagens.Tamanho14));

                RuleFor(x => x.DataFundacao)
                    .NotEmpty().WithMessage(string.Format(Mensagens.CampoObrigatorio, Mensagens.CampoDataFundacao))
                    .NotNull()
                    .Must(x => x > DateTime.MinValue).WithMessage(string.Format(Mensagens.CampoDevePossuirTamanhoSuperior, Mensagens.CampoDataFundacao, DateTime.MinValue.ToString()));
            }
        }
    }
}
