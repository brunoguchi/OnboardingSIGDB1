using Bogus;
using Bogus.Extensions.Brazil;
using OnboardingSIGDB1.Domain.Funcionarios.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using OnboardingSIGDB1.Core.Extensions;

namespace OnboardingSIGDB1.Tests.Builders
{
    public class FuncionarioBuilder
    {
        public int Id { get; set; }
        public string Nome { get; private set; }
        public string Cpf { get; private set; }
        public int? EmpresaId { get; set; }
        public DateTime DataContratacao { get; private set; }

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

        public Funcionario Build()
        {
            var empresa = new Funcionario(Nome, Cpf, DataContratacao, EmpresaId);

            if (Id <= 0) return empresa;

            empresa.AtualizarId(this.Id);

            return empresa;
        }
    }
}
