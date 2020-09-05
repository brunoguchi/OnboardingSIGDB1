using Bogus;
using OnboardingSIGDB1.Domain.Cargos.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnboardingSIGDB1.Tests.Builders
{
    public class CargoBuilder
    {
        protected int Id;
        protected string Descricao;

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

            var propertyInfo = cargo.GetType().GetProperty("Id");
            propertyInfo.SetValue(cargo, Convert.ChangeType(Id, propertyInfo.PropertyType), null);

            return cargo;
        }
    }
}
