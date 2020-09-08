using Bogus;
using Bogus.Extensions.Brazil;
using OnboardingSIGDB1.Domain.Cargos.Entidades;
using OnboardingSIGDB1.Domain.Empresas.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using OnboardingSIGDB1.Core.Extensions;

namespace OnboardingSIGDB1.Tests.Builders
{
    public class EmpresaBuilder
    {
        public int Id { get; set; }
        public string Nome { get; private set; }
        public string Cnpj { get; private set; }
        public DateTime DataFundacao { get; private set; }

        public static EmpresaBuilder Novo()
        {
            var _faker = new Faker();

            return new EmpresaBuilder
            {
                Nome = _faker.Company.CompanyName(),
                Cnpj = _faker.Company.Cnpj(),
                DataFundacao = DateTime.Now
            };
        }

        public EmpresaBuilder ComId(int id)
        {
            Id = id;
            return this;
        }

        public EmpresaBuilder ComCnpjSemFormatacao()
        {
            Cnpj = Cnpj.RemoverFormatacaoDocumento();
            return this;
        }

        public Empresa Build()
        {
            var empresa = new Empresa(Nome, Cnpj, DataFundacao);

            if (Id <= 0) return empresa;

            empresa.AtualizarId(this.Id);

            return empresa;
        }
    }
}
