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
using Xunit;
using OnboardingSIGDB1.Core.Extensions;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Tests.Funcionarios
{
    public class AtualizadorDeFuncionariosTests
    {
        private readonly Faker _faker;
        private readonly FuncionarioDto funcionarioDto;
        private readonly AtualizadorDeFuncionarios atualizadorDeFuncionarios;

        private readonly Mock<IRepositorioBase<Funcionario>> repositorioBase;
        private readonly Mock<NotificationContext> notificationContext;

        public AtualizadorDeFuncionariosTests()
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
            atualizadorDeFuncionarios = new AtualizadorDeFuncionarios(repositorioBase.Object, notificationContext.Object);
        }

        [Fact]
        public async Task DeveEditarFuncionario()
        {
            var funcionario = FuncionarioBuilder.Novo().ComId(1).Build();
            repositorioBase.Setup(x => x.GetById(funcionarioDto.Id)).ReturnsAsync(funcionario);
            funcionarioDto.Cpf = funcionarioDto.Cpf.RemoverFormatacaoDocumento();

            await atualizadorDeFuncionarios.Atualizar(funcionarioDto);

            Assert.Equal(funcionarioDto.Nome, funcionario.Nome);
            Assert.Equal(funcionarioDto.Cpf, funcionario.Cpf);
            Assert.Equal(funcionarioDto.DataContratacao, funcionario.DataContratacao);
        }

        [Fact]
        public async Task DeveNotificarQuandoNaoExistirEmpresaParaEdicao()
        {
            Funcionario funcionarioNaoLocalizado = null;
            repositorioBase.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(funcionarioNaoLocalizado);

            await atualizadorDeFuncionarios.Atualizar(funcionarioDto);

            Assert.True(notificationContext.Object.HasNotifications);
        }
    }
}
