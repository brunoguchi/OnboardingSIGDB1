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
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OnboardingSIGDB1.Tests.Cargos
{
    public class RemovedorDeCargosTests
    {
        private readonly Faker _faker;
        private readonly CargoDto _cargoDto;
        private readonly RemovedorDeCargos _removedorDeCargos;

        private readonly Mock<IRepositorioBase<Cargo>> repositorioBase;
        private readonly Mock<NotificationContext> notificationContext;

        public RemovedorDeCargosTests()
        {
            _faker = new Faker();
            _cargoDto = new CargoDto
            {
                Descricao = _faker.Name.JobDescriptor()
            };

            repositorioBase = new Mock<IRepositorioBase<Cargo>>();
            notificationContext = new Mock<NotificationContext>();
            _removedorDeCargos = new RemovedorDeCargos(repositorioBase.Object, notificationContext.Object);
        }

        [Fact]
        public async Task DeveDeletarCargo()
        {
            var cargoId = 1;
            var cargo = CargoBuilder.Novo().ComId(cargoId).Build();
            repositorioBase.Setup(x => x.GetById(cargoId)).ReturnsAsync(cargo);

            await _removedorDeCargos.Deletar(cargoId);

            repositorioBase.Verify(x => x.Remove(It.IsAny<Cargo>()), Times.Once);
        }

        [Fact]
        public async Task DeveNotificarQuandoNaoExistirCargoParaDelecao()
        {
            var cargoId = 1;
            Cargo cargoNaoLocalizado = null;
            repositorioBase.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(cargoNaoLocalizado);

            await _removedorDeCargos.Deletar(cargoId);

            Assert.True(notificationContext.Object.HasNotifications);
        }
    }
}
