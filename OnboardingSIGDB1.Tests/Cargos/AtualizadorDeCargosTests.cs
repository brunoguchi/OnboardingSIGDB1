using AutoMapper;
using Bogus;
using Moq;
using OnboardingSIGDB1.Core.Notifications;
using OnboardingSIGDB1.Domain.Base.Interfaces;
using OnboardingSIGDB1.Domain.Cargos.Dtos;
using OnboardingSIGDB1.Domain.Cargos.Entidades;
using OnboardingSIGDB1.Domain.Cargos.Servicos;
using OnboardingSIGDB1.Tests.Builders;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OnboardingSIGDB1.Tests.Cargos
{
    public class AtualizadorDeCargosTests
    {
        private readonly Faker _faker;
        private readonly CargoDto _cargoDto;
        private readonly AtualizadorDeCargos _atualizadorDeCargos;

        private readonly Mock<IRepositorioBase<Cargo>> repositorioBase;
        private readonly Mock<NotificationContext> notificationContext;

        public AtualizadorDeCargosTests()
        {
            _faker = new Faker();
            _cargoDto = new CargoDto
            {
                Descricao = _faker.Name.JobDescriptor()
            };

            repositorioBase = new Mock<IRepositorioBase<Cargo>>();
            notificationContext = new Mock<NotificationContext>();
            _atualizadorDeCargos = new AtualizadorDeCargos(repositorioBase.Object, notificationContext.Object);
        }

        [Fact]
        public async Task DeveEditarCargo()
        {
            var cargo = CargoBuilder.Novo().ComId(1).Build();
            repositorioBase.Setup(x => x.GetById(_cargoDto.Id)).ReturnsAsync(cargo);

            await _atualizadorDeCargos.Atualizar(_cargoDto);

            Assert.Equal(_cargoDto.Descricao, cargo.Descricao);
        }
    }
}
