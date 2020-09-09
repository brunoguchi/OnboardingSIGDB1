using Bogus;
using Bogus.Extensions.Brazil;
using OnboardingSIGDB1.Domain.Funcionarios.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using OnboardingSIGDB1.Core.Extensions;
using OnboardingSIGDB1.Domain.Cargos.Entidades;

namespace OnboardingSIGDB1.Tests.Builders
{
    public class FuncionarioBuilder
    {
        public int Id { get; set; }
        public string Nome { get; private set; }
        public string Cpf { get; private set; }
        public int? EmpresaId { get; set; }
        public DateTime DataContratacao { get; private set; }
        public Cargo Cargo { get; set; }
        public DateTime DataDeVinculo { get; set; }

        public static FuncionarioBuilder Novo()
        {
            var _faker = new Faker();

            return new FuncionarioBuilder
            {
                Nome = _faker.Person.FullName,
                Cpf = _faker.Person.Cpf(),
                DataContratacao = DateTime.Now
            };
        }

        public FuncionarioBuilder ComId(int id)
        {
            Id = id;
            return this;
        }

        public FuncionarioBuilder ComCnpjSemFormatacao()
        {
            Cpf = Cpf.RemoverFormatacaoDocumento();
            return this;
        }

        public FuncionarioBuilder ComEmpresa(int? id)
        {
            EmpresaId = id;
            return this;
        }

        public FuncionarioBuilder ComCargo(Cargo cargo, DateTime dataVinculo)
        {
            this.Cargo = cargo;
            this.DataDeVinculo = dataVinculo;
            return this;
        }

        public Funcionario Build()
        {
            var funcionario = new Funcionario(Nome, Cpf, DataContratacao, EmpresaId);

            if (Cargo != null)
                funcionario.AdicionarCargo(this.Cargo, this.DataDeVinculo);

            if (Id <= 0) return funcionario;

            funcionario.AtualizarId(this.Id);

            return funcionario;
        }
    }
}
