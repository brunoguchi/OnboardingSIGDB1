using Bogus;
using Bogus.Extensions.Brazil;
using Moq;
using OnboardingSIGDB1.Core.Notifications;
using OnboardingSIGDB1.Domain.Base.Interfaces;
using OnboardingSIGDB1.Domain.Funcionarios.Dtos;
using OnboardingSIGDB1.Domain.Funcionarios.Entidades;
using OnboardingSIGDB1.Domain.Funcionarios.Servicos;
using OnboardingSIGDB1.Tests.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using OnboardingSIGDB1.Core.Extensions;
using System.Threading.Tasks;
using Xunit;
using System.Runtime.CompilerServices;

namespace OnboardingSIGDB1.Tests.Funcionarios
{
    public class VinculadorDeFuncionarioEmpresaTests
    {
        private readonly Mock<IRepositorioBase<Funcionario>> repositorioBase;
        private readonly Mock<NotificationContext> notificationContext;
        private readonly VinculadorDeFuncionarioEmpresa vinculadorDeFuncionarioEmpresa;
        private readonly Faker _faker;
        private readonly FuncionarioDto funcionarioDto;

        public VinculadorDeFuncionarioEmpresaTests()
        {
            this.repositorioBase = new Mock<IRepositorioBase<Funcionario>>();
            this.notificationContext = new Mock<NotificationContext>();
            this._faker = new Faker();

            funcionarioDto = new FuncionarioDto
            {
                Id = _faker.Random.Int(min: 1, max: 100),
                Nome = _faker.Person.FullName,
                Cpf = _faker.Person.Cpf(),
                DataContratacao = DateTime.Now,
                EmpresaId = _faker.Random.Int(min: 1, max: 10)
            };

            vinculadorDeFuncionarioEmpresa = new VinculadorDeFuncionarioEmpresa(repositorioBase.Object, notificationContext.Object);
        }

        [Fact]
        public async Task DeveVincularFuncionarioAEmpresa()
        {
            var funcionario = FuncionarioBuilder.Novo().ComId(funcionarioDto.Id).Build();
            repositorioBase.Setup(x => x.GetById(funcionarioDto.Id)).ReturnsAsync(funcionario);

            await vinculadorDeFuncionarioEmpresa.VincularFuncionarioAEmpresa(funcionarioDto);

            Assert.Equal(funcionarioDto.EmpresaId, funcionario.EmpresaId);
        }

        [Fact]
        public async Task DeveNotificarQuandoNaoExistirFuncionario()
        {
            Funcionario funcionarioInvalido = null;
            repositorioBase.Setup(x => x.GetById(funcionarioDto.Id)).ReturnsAsync(funcionarioInvalido);

            await vinculadorDeFuncionarioEmpresa.VincularFuncionarioAEmpresa(funcionarioDto);

            Assert.True(notificationContext.Object.HasNotifications);
        }

        [Fact]
        public async Task DeveNotificarQuandoFuncionarioJaPossuiVinculoComEmpresa()
        {
            var funcionario = FuncionarioBuilder.Novo().ComId(funcionarioDto.Id).ComEmpresa(funcionarioDto.EmpresaId).Build();
            repositorioBase.Setup(x => x.GetById(funcionarioDto.Id)).ReturnsAsync(funcionario);

            await vinculadorDeFuncionarioEmpresa.VincularFuncionarioAEmpresa(funcionarioDto);

            Assert.True(notificationContext.Object.HasNotifications);
        }
    }
}
