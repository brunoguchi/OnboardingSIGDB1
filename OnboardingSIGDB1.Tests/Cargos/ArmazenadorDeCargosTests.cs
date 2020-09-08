using AutoMapper;
using Bogus;
using OnboardingSIGDB1.Core.Notifications;
using OnboardingSIGDB1.Domain.Base.Interfaces;
using OnboardingSIGDB1.Domain.Cargos.Dtos;
using OnboardingSIGDB1.Domain.Cargos.Entidades;
using OnboardingSIGDB1.Domain.Cargos.Servicos;
using Moq;
using Xunit;
using System.Threading.Tasks;
using OnboardingSIGDB1.Tests.Builders;
using System.Reflection;
using System.Linq;
using OnboardingSIGDB1.Core.Resources;

namespace OnboardingSIGDB1.Tests.Cargos
{
    public class ArmazenadorDeCargosTests
    {
        private readonly Faker _faker;
        private readonly CargoDto _cargoDto;
        private readonly ArmazenadorDeCargos _armazenadorDeCargos;

        private readonly Mock<IRepositorioBase<Cargo>> repositorioBase;
        private readonly Mock<NotificationContext> notificationContext;
        private readonly Mock<IMapper> iMapper;

        public ArmazenadorDeCargosTests()
        {
            _faker = new Faker();
            _cargoDto = new CargoDto
            {
                Descricao = _faker.Name.JobDescriptor()
            };

            repositorioBase = new Mock<IRepositorioBase<Cargo>>();
            notificationContext = new Mock<NotificationContext>();
            iMapper = new Mock<IMapper>();

            iMapper.Setup(x => x.Map<Cargo>(It.IsAny<CargoDto>()))
                .Returns((CargoDto source) => new Cargo(source.Descricao));

            _armazenadorDeCargos = new ArmazenadorDeCargos(repositorioBase.Object, notificationContext.Object, iMapper.Object);
        }

        [Fact]
        public void DeveCriarCargo()
        {
            var cargoDto = new CargoDto { Descricao = _faker.Name.JobDescriptor() };
            var cargo = CargoBuilder.Novo().Build();

            cargo.AtualizarDescricao(cargoDto.Descricao);

            Assert.Equal(cargoDto.Descricao, cargo.Descricao);
        }

        [Fact]
        public async Task DeveAdicionarCargo()
        {
            await _armazenadorDeCargos.Adicionar(_cargoDto);

            repositorioBase.Verify(x => x.Add(It.IsAny<Cargo>()), Times.Once);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public async Task NaoDeveAdicionarCargoComDescricaoInvalida(string descricao)
        {
            //Arrange
            var cargo = CargoBuilder.Novo().Build();
            _cargoDto.Descricao = descricao;
            bool resultadoEsperado = true;

            //Act
            await _armazenadorDeCargos.Adicionar(_cargoDto);

            //Assert
            repositorioBase.Verify(x => x.Add(It.IsAny<Cargo>()), Times.Never);
            Assert.Equal(notificationContext
                .Object
                .Notifications
                .Any(x => x.Mensagem.Equals(string.Format(Mensagens.CampoObrigatorio, Mensagens.CampoDescricao))), resultadoEsperado);
        }
    }
}
