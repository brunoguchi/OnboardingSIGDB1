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
using System.Threading.Tasks;
using Xunit;

namespace OnboardingSIGDB1.Tests.Funcionarios
{
    public class RemovedorDeFuncionariosTests
    {
        private readonly Faker _faker;
        private readonly FuncionarioDto funcionarioDto;
        private readonly RemovedorDeFuncionarios removedorDeFuncionarios;

        private readonly Mock<IRepositorioBase<Funcionario>> repositorioBase;
        private readonly Mock<NotificationContext> notificationContext;

        public RemovedorDeFuncionariosTests()
        {
            _faker = new Faker();
            funcionarioDto = new FuncionarioDto
            {
                Nome = _faker.Person.FullName,
                Cpf = _faker.Person.Cpf(),
                DataContratacao = DateTime.Now
            };

            repositorioBase = new Mock<IRepositorioBase<Funcionario>>();
            notificationContext = new Mock<NotificationContext>();
            removedorDeFuncionarios = new RemovedorDeFuncionarios(repositorioBase.Object, notificationContext.Object);
        }

        [Fact]
        public async Task DeveDeletarEmpresa()
        {
            var funcionarioId = 1;
            var empresa = FuncionarioBuilder.Novo().ComId(funcionarioId).Build();
            repositorioBase.Setup(x => x.GetById(funcionarioId)).ReturnsAsync(empresa);

            await removedorDeFuncionarios.Deletar(funcionarioId);

            repositorioBase.Verify(x => x.Remove(It.IsAny<Funcionario>()), Times.Once);
        }

        [Fact]
        public async Task DeveNotificarQuandoNaoExistirEmpresaParaDelecao()
        {
            var funcionarioId = 10;
            Funcionario empresaNaoLocalizada = null;
            repositorioBase.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(empresaNaoLocalizada);

            await removedorDeFuncionarios.Deletar(funcionarioId);

            Assert.True(notificationContext.Object.HasNotifications);
        }
    }
}
