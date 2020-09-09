using AutoMapper;
using Bogus;
using Moq;
using OnboardingSIGDB1.Core.Notifications;
using OnboardingSIGDB1.Core.Resources;
using OnboardingSIGDB1.Domain.Base.Interfaces;
using OnboardingSIGDB1.Domain.Cargos.Dtos;
using OnboardingSIGDB1.Domain.Cargos.Entidades;
using OnboardingSIGDB1.Domain.Cargos.Servicos;
using OnboardingSIGDB1.Tests.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
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
                Id = _faker.Random.Int(min: 1, max: 100),
                Descricao = _faker.Name.JobDescriptor()
            };

            repositorioBase = new Mock<IRepositorioBase<Cargo>>();
            notificationContext = new Mock<NotificationContext>();
            _atualizadorDeCargos = new AtualizadorDeCargos(repositorioBase.Object, notificationContext.Object);
        }

        [Fact]
        public async Task DeveEditarCargo()
        {
            var cargo = CargoBuilder.Novo().ComId(_cargoDto.Id).Build();
            repositorioBase.Setup(x => x.GetById(_cargoDto.Id)).ReturnsAsync(cargo);

            await _atualizadorDeCargos.Atualizar(_cargoDto);

            Assert.Equal(_cargoDto.Descricao, cargo.Descricao);
        }

        [Fact]
        public async Task DeveNotificarQuandoNaoExistirCargoParaEdicao()
        {
            Cargo cargoNaoLocalizado = null;
            repositorioBase.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(cargoNaoLocalizado);

            await _atualizadorDeCargos.Atualizar(_cargoDto);

            Assert.True(notificationContext.Object.HasNotifications);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public async Task NaoDeveEditarCargoComDescricaoInvalida(string descricao)
        {
            //Arrange
            var cargo = CargoBuilder.Novo().ComId(_cargoDto.Id).Build();
            repositorioBase.Setup(x => x.GetById(_cargoDto.Id)).ReturnsAsync(cargo);
            _cargoDto.Descricao = descricao;
            bool resultadoEsperado = true;

            //Act
            await _atualizadorDeCargos.Atualizar(_cargoDto);

            //Assert
            repositorioBase.Verify(x => x.Add(It.IsAny<Cargo>()), Times.Never);
            Assert.Equal(notificationContext
                .Object
                .Notifications
                .Any(x => x.Mensagem.Equals(string.Format(Mensagens.CampoObrigatorio, Mensagens.CampoDescricao))), resultadoEsperado);
        }
    }
}
