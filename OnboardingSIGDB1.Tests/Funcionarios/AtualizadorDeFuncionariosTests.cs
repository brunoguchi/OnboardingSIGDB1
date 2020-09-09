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
using System.Linq;
using OnboardingSIGDB1.Core.Resources;

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
                Id = _faker.Random.Int(min: 1, max: 100),
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
            var funcionario = FuncionarioBuilder.Novo().ComId(funcionarioDto.Id).Build();
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

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public async Task NaoDeveAdicionarFuncionarioComNomeInvalido(string nome)
        {
            var funcionario = FuncionarioBuilder.Novo().ComId(funcionarioDto.Id).Build();
            repositorioBase.Setup(x => x.GetById(funcionarioDto.Id)).ReturnsAsync(funcionario);
            funcionarioDto.Nome = nome;
            bool resultadoEsperado = true;

            await atualizadorDeFuncionarios.Atualizar(funcionarioDto);

            repositorioBase.Verify(x => x.Add(It.IsAny<Funcionario>()), Times.Never);
            Assert.Equal(notificationContext
                .Object
                .Notifications
                .Any(x => x.Mensagem.Equals(string.Format(Mensagens.CampoObrigatorio, Mensagens.CampoNome))), resultadoEsperado);
        }

        [Theory]
        [InlineData(155)]
        [InlineData(200)]
        public async Task NaoDeveAdicionarFuncionarioComTamanhoMaximoDeNomeExcedido(int tamanho)
        {
            var funcionario = FuncionarioBuilder.Novo().ComId(funcionarioDto.Id).Build();
            repositorioBase.Setup(x => x.GetById(funcionarioDto.Id)).ReturnsAsync(funcionario);
            bool resultadoEsperado = true;
            funcionarioDto.Nome = _faker.Lorem.Letter(tamanho);

            await atualizadorDeFuncionarios.Atualizar(funcionarioDto);

            repositorioBase.Verify(x => x.Add(It.IsAny<Funcionario>()), Times.Never);
            Assert.Equal(notificationContext
                .Object
                .Notifications
                .Any(x => x.Mensagem.Equals(string.Format(Mensagens.CampoComTamanhoMaximo, Mensagens.CampoNome, Mensagens.Tamanho150))), resultadoEsperado);
        }

        [Fact]
        public async Task NaoDeveAdicionarFuncionarioComDataContratacaoMenorOuIgualAoValorMinimo()
        {
            var funcionario = FuncionarioBuilder.Novo().ComId(funcionarioDto.Id).Build();
            repositorioBase.Setup(x => x.GetById(funcionarioDto.Id)).ReturnsAsync(funcionario);
            bool resultadoEsperado = true;
            funcionarioDto.DataContratacao = DateTime.MinValue;

            await atualizadorDeFuncionarios.Atualizar(funcionarioDto);

            repositorioBase.Verify(x => x.Add(It.IsAny<Funcionario>()), Times.Never);
            Assert.Equal(notificationContext
                .Object
                .Notifications
                .Any(x => x.Mensagem.Equals(string.Format(Mensagens.CampoDevePossuirTamanhoSuperior, Mensagens.CampoDataContratacao, funcionarioDto.DataContratacao.ToString()))), resultadoEsperado);
        }

        [Fact]
        public async Task NaoDeveAdicionarFuncionarioSeTiverNotificacao()
        {
            await atualizadorDeFuncionarios.Atualizar(funcionarioDto);

            repositorioBase.Verify(x => x.Add(It.IsAny<Funcionario>()), Times.Never);
            Assert.True(notificationContext.Object.HasNotifications);
        }

        [Theory]
        [InlineData("206.349.03098-4323")]
        [InlineData("410.454.840-5923")]
        [InlineData("174.5422.30-029")]
        public async Task NaoDeveAdicionarFuncionarioComCpfDeTamanhoExcedido(string cpf)
        {
            var funcionario = FuncionarioBuilder.Novo().ComId(funcionarioDto.Id).Build();
            repositorioBase.Setup(x => x.GetById(funcionarioDto.Id)).ReturnsAsync(funcionario);
            bool resultadoEsperado = true;
            funcionarioDto.Cpf = cpf;

            await atualizadorDeFuncionarios.Atualizar(funcionarioDto);

            repositorioBase.Verify(x => x.Add(It.IsAny<Funcionario>()), Times.Never);
            Assert.Equal(notificationContext
                .Object
                .Notifications
                .Any(x => x.Mensagem.Equals(string.Format(Mensagens.CampoComTamanhoMaximo, Mensagens.CampoCPF, Mensagens.Tamanho11))), resultadoEsperado);
        }

        [Theory]
        [InlineData("20.349.03098")]
        [InlineData("41.44.84-59")]
        [InlineData("174.5422.00")]
        public async Task NaoDeveAdicionarFuncionarioComCpfDeTamanhoAbaixoDoPermitido(string cpf)
        {
            var funcionario = FuncionarioBuilder.Novo().ComId(funcionarioDto.Id).Build();
            repositorioBase.Setup(x => x.GetById(funcionarioDto.Id)).ReturnsAsync(funcionario);
            bool resultadoEsperado = true;
            funcionarioDto.Cpf = cpf;

            await atualizadorDeFuncionarios.Atualizar(funcionarioDto);

            repositorioBase.Verify(x => x.Add(It.IsAny<Funcionario>()), Times.Never);
            Assert.Equal(notificationContext
                .Object
                .Notifications
                .Any(x => x.Mensagem.Equals(string.Format(Mensagens.CampoComTamanhoFixo, Mensagens.CampoCPF, Mensagens.Tamanho11))), resultadoEsperado);
        }
    }
}
