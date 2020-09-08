using Bogus;
using Bogus.Extensions.Brazil;
using Moq;
using OnboardingSIGDB1.Core.Notifications;
using OnboardingSIGDB1.Domain.Funcionarios.Dtos;
using OnboardingSIGDB1.Domain.Funcionarios.Interfaces.Repositorios;
using OnboardingSIGDB1.Domain.Servicos;
using OnboardingSIGDB1.Tests.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OnboardingSIGDB1.Tests.Funcionarios
{
    public class ServicoDeValidacaoDeFuncionariosTests
    {
        private readonly Mock<IConsultaDeFuncionarios> consultaDeFuncionarios;
        private readonly Mock<NotificationContext> notificationContextMock;
        private readonly ServicoDeValidacaoDeFuncionarios servicoDeValidacaoDeFuncionarios;
        private readonly Faker _faker;

        public ServicoDeValidacaoDeFuncionariosTests()
        {
            this.consultaDeFuncionarios = new Mock<IConsultaDeFuncionarios>();
            this.notificationContextMock = new Mock<NotificationContext>();
            _faker = new Faker();

            servicoDeValidacaoDeFuncionarios = new ServicoDeValidacaoDeFuncionarios(consultaDeFuncionarios.Object, notificationContextMock.Object);
        }

        [Fact]
        public async Task DeveNotificarQuandoFuncionarioJaEstiverCadastradoComMesmoCPF()
        {
            var funcionarioDto = new FuncionarioDto { Id = _faker.Random.Int(), Cpf = _faker.Person.Cpf(), DataContratacao = DateTime.Now };
            var funcionario = FuncionarioBuilder.Novo().Build();
            funcionario.AtualizarCpf(funcionarioDto.Cpf);
            consultaDeFuncionarios.Setup(x => x.RecuperarPorCpf(It.IsAny<string>())).ReturnsAsync(funcionarioDto);

            await servicoDeValidacaoDeFuncionarios.Executar(funcionario);

            Assert.True(notificationContextMock.Object.HasNotifications);
        }
    }
}
