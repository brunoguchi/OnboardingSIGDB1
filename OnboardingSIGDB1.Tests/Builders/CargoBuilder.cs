using Bogus;
using OnboardingSIGDB1.Domain.Cargos.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnboardingSIGDB1.Tests.Builders
{
    public class CargoBuilder
    {
        public int Id { get; set; }
        public string Descricao { get; set; }

        public static CargoBuilder Novo()
        {
            var _faker = new Faker();

            return new CargoBuilder
            {
                Descricao = _faker.Name.JobDescriptor()
            };
        }

        public CargoBuilder ComId(int id)
        {
            Id = id;
            return this;
        }

        public Cargo Build()
        {
            var cargo = new Cargo(Descricao);

            if (Id <= 0) return cargo;

            cargo.AtualizarId(this.Id);

            return cargo;
        }
    }
}
