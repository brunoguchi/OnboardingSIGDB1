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
                .Returns((CargoDto source) => new Cargo() { Descricao = source.Descricao });

            _armazenadorDeCargos = new ArmazenadorDeCargos(repositorioBase.Object, notificationContext.Object, iMapper.Object);
        }

        [Fact]
        public async Task DeveAdicionarCargo()
        {
            await _armazenadorDeCargos.Adicionar(_cargoDto);

            repositorioBase.Verify(x => x.Add(It.Is<Cargo>(a => a.Valid)));
        }
    }
}
