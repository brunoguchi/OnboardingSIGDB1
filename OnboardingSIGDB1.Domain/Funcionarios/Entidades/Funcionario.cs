using FluentValidation;
using OnboardingSIGDB1.Core.Extensions;
using OnboardingSIGDB1.Core.Resources;
using OnboardingSIGDB1.Domain.Base.Entidades;
using OnboardingSIGDB1.Domain.Cargos.Entidades;
using OnboardingSIGDB1.Domain.Empresas.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace OnboardingSIGDB1.Domain.Funcionarios.Entidades
{
    public class Funcionario : EntidadeValidacao
    {
        public string Nome { get; private set; }
        public string Cpf { get; private set; }
        public DateTime DataContratacao { get; private set; }
        public int? EmpresaId { get; private set; }
        public Empresa Empresa { get; private set; }
        public virtual List<FuncionarioCargo> FuncionariosCargos { get; private set; } = new List<FuncionarioCargo>();

        public bool Validar()
        {
            return base.Validate(this, new FuncionarioValidator());
        }

        public Funcionario() { }
        public Funcionario(string nome, string cpf, DateTime dataContratacao, int? empresaId = null)
        {
            this.Nome = nome;
            this.Cpf = cpf;
            this.DataContratacao = dataContratacao;
            this.EmpresaId = empresaId;
        }

        public void AtualizarNome(string nome) => this.Nome = nome;
        public void AtualizarCpf(string cpf) => this.Cpf = cpf.RemoverFormatacaoDocumento();
        public void AtualizarDataContratacao(DateTime data) => this.DataContratacao = data;
        public void AtualizarEmpresaId(int? empresaId) => this.EmpresaId = empresaId;
        public void AdicionarCargo(Cargo cargo, DateTime dataVinculo)
        {
            this.FuncionariosCargos.Add(new FuncionarioCargo(this, cargo, dataVinculo));
        }

        public class FuncionarioValidator : AbstractValidator<Funcionario>
        {
            public FuncionarioValidator()
            {
                RuleFor(x => x.Nome)
                    .NotEmpty().WithMessage(string.Format(Mensagens.CampoObrigatorio, Mensagens.CampoNome))
                    .MaximumLength(150).WithMessage(string.Format(Mensagens.CampoComTamanhoMaximo, Mensagens.CampoNome, Mensagens.Tamanho150));

                RuleFor(x => x.Cpf)
                    .NotEmpty().WithMessage(string.Format(Mensagens.CampoObrigatorio, Mensagens.CampoCPF))
                    .NotNull().WithMessage(string.Format(Mensagens.CampoObrigatorio, Mensagens.CampoCPF))
                    .MaximumLength(11).WithMessage(string.Format(Mensagens.CampoComTamanhoMaximo, Mensagens.CampoCPF, Mensagens.Tamanho11))
                    .Length(11).WithMessage(string.Format(Mensagens.CampoComTamanhoFixo, Mensagens.CampoCPF, Mensagens.Tamanho11));

                RuleFor(x => x.DataContratacao)
                    .NotEmpty().WithMessage(string.Format(Mensagens.CampoObrigatorio, Mensagens.CampoDataContratacao))
                    .Must(x => x > DateTime.MinValue).WithMessage(string.Format(Mensagens.CampoDevePossuirTamanhoSuperior, Mensagens.CampoDataContratacao, DateTime.MinValue.ToString()));
            }
        }
    }
}
