using Bogus;
using Bogus.Extensions.Brazil;
using Moq;
using OnboardingSIGDB1.Core.Notifications;
using OnboardingSIGDB1.Domain.Base.Interfaces;
using OnboardingSIGDB1.Domain.Cargos.Entidades;
using OnboardingSIGDB1.Domain.Funcionarios.Dtos;
using OnboardingSIGDB1.Domain.Funcionarios.Entidades;
using OnboardingSIGDB1.Domain.Funcionarios.Interfaces.Repositorios;
using OnboardingSIGDB1.Domain.Funcionarios.Servicos;
using OnboardingSIGDB1.Tests.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OnboardingSIGDB1.Tests.Funcionarios
{
    public class VinculadorDeFuncionarioCargoTests
    {
        private readonly Mock<IRepositorioDeFuncionarios> repositorioDeFuncionarios;
        private readonly Mock<IRepositorioBase<Cargo>> repositorioCargoBase;
        private readonly Mock<NotificationContext> notificationContext;
        private readonly VinculadorDeFuncionarioCargo vinculadorDeFuncionarioCargo;
        private readonly Faker _faker;
        private readonly FuncionarioCargoDto funcionarioCargoDto;

        public VinculadorDeFuncionarioCargoTests()
        {
            this.repositorioDeFuncionarios = new Mock<IRepositorioDeFuncionarios>();
            this.repositorioCargoBase = new Mock<IRepositorioBase<Cargo>>();
            this.notificationContext = new Mock<NotificationContext>();
            this._faker = new Faker();

            funcionarioCargoDto = new FuncionarioCargoDto
            {
                CargoId = _faker.Random.Int(min: 1, max: 100),
                FuncionarioId = _faker.Random.Int(min: 1, max: 100),
                DataDeVinculo = _faker.Date.Past()

            };

            vinculadorDeFuncionarioCargo = new VinculadorDeFuncionarioCargo(repositorioDeFuncionarios.Object, repositorioCargoBase.Object, notificationContext.Object);
        }

        [Fact]
        public async Task DeveVincularFuncionarioAoCargo()
        {
            var cargo = CargoBuilder.Novo().ComId(funcionarioCargoDto.CargoId).Build();
            var funcionario = FuncionarioBuilder.Novo().ComId(funcionarioCargoDto.FuncionarioId).ComEmpresa(1).Build();
            repositorioDeFuncionarios.Setup(x => x.RecuperarPorIdComCargos(funcionarioCargoDto.FuncionarioId)).ReturnsAsync(funcionario);
            repositorioCargoBase.Setup(x => x.GetById(funcionarioCargoDto.CargoId)).ReturnsAsync(cargo);

            await vinculadorDeFuncionarioCargo.VincularFuncionarioAoCargo(funcionarioCargoDto);

            Assert.True(funcionario.FuncionariosCargos.Where(x => x.CargoId == cargo.Id).Any());
        }

        [Fact]
        public async Task DeveNotificarSeFuncionarioNaoExistir()
        {
            Funcionario funcionarioInvalido = null;
            var cargo = CargoBuilder.Novo().ComId(funcionarioCargoDto.CargoId).Build();
            repositorioDeFuncionarios.Setup(x => x.RecuperarPorIdComCargos(funcionarioCargoDto.FuncionarioId)).ReturnsAsync(funcionarioInvalido);
            repositorioCargoBase.Setup(x => x.GetById(funcionarioCargoDto.CargoId)).ReturnsAsync(cargo);

            await vinculadorDeFuncionarioCargo.VincularFuncionarioAoCargo(funcionarioCargoDto);

            Assert.True(notificationContext.Object.HasNotifications);
        }

        [Fact]
        public async Task DeveNotificarSeCargoNaoExistir()
        {
            var funcionario = FuncionarioBuilder.Novo().ComId(funcionarioCargoDto.FuncionarioId).ComEmpresa(1).Build();
            Cargo cargoInvalido = null;
            repositorioDeFuncionarios.Setup(x => x.RecuperarPorIdComCargos(funcionarioCargoDto.FuncionarioId)).ReturnsAsync(funcionario);
            repositorioCargoBase.Setup(x => x.GetById(funcionarioCargoDto.CargoId)).ReturnsAsync(cargoInvalido);

            await vinculadorDeFuncionarioCargo.VincularFuncionarioAoCargo(funcionarioCargoDto);

            Assert.True(notificationContext.Object.HasNotifications);
        }

        [Fact]
        public async Task DeveNotificarSeFuncionarioNaoTiverVinculoComEmpresa()
        {
            var funcionario = FuncionarioBuilder.Novo().ComId(funcionarioCargoDto.FuncionarioId).Build();
            var cargo = CargoBuilder.Novo().ComId(funcionarioCargoDto.CargoId).Build();

            repositorioDeFuncionarios.Setup(x => x.RecuperarPorIdComCargos(funcionarioCargoDto.FuncionarioId)).ReturnsAsync(funcionario);
            repositorioCargoBase.Setup(x => x.GetById(funcionarioCargoDto.CargoId)).ReturnsAsync(cargo);

            await vinculadorDeFuncionarioCargo.VincularFuncionarioAoCargo(funcionarioCargoDto);

            Assert.True(notificationContext.Object.HasNotifications);
        }

        [Fact]
        public async Task DeveNotificarSeFuncionarioJaPossuiVinculoComCargoSelecionado()
        {
            var cargo = CargoBuilder.Novo().ComId(funcionarioCargoDto.CargoId).Build();
            var funcionario = FuncionarioBuilder.Novo().ComId(funcionarioCargoDto.FuncionarioId).ComEmpresa(1).ComCargo(cargo, funcionarioCargoDto.DataDeVinculo).Build();
            repositorioDeFuncionarios.Setup(x => x.RecuperarPorIdComCargos(funcionarioCargoDto.FuncionarioId)).ReturnsAsync(funcionario);
            repositorioCargoBase.Setup(x => x.GetById(funcionarioCargoDto.CargoId)).ReturnsAsync(cargo);

            await vinculadorDeFuncionarioCargo.VincularFuncionarioAoCargo(funcionarioCargoDto);

            Assert.True(notificationContext.Object.HasNotifications);
        }
    }
}
