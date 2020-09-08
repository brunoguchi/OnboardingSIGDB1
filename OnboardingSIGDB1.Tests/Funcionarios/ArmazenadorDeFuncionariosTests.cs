using AutoMapper;
using Bogus;
using Bogus.Extensions.Brazil;
using Moq;
using OnboardingSIGDB1.Core.Notifications;
using OnboardingSIGDB1.Domain.Base.Interfaces;
using OnboardingSIGDB1.Domain.Funcionarios.Dtos;
using OnboardingSIGDB1.Domain.Funcionarios.Entidades;
using OnboardingSIGDB1.Domain.Funcionarios.Interfaces.Validadores;
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
    public class ArmazenadorDeFuncionariosTests
    {
        private readonly Faker _faker;
        private readonly FuncionarioDto _funcionarioDto;
        private readonly ArmazenadorDeFuncionarios _armazenadorDeFuncionarios;
        private readonly Mock<IRepositorioBase<Funcionario>> _repositorioBaseMockMock;
        private readonly Mock<NotificationContext> _notificationContextMock;
        private readonly Mock<IMapper> _iMapperMock;
        private readonly Mock<IServicoDeValidacaoDeFuncionarios> _servicoDeValidacaoDeFuncionariosMock;

        public ArmazenadorDeFuncionariosTests()
        {
            _faker = new Faker();
            _funcionarioDto = new FuncionarioDto
            {
                Nome = _faker.Person.FullName,
                Cpf = _faker.Person.Cpf(),
                DataContratacao = DateTime.Now
            };

            _repositorioBaseMockMock = new Mock<IRepositorioBase<Funcionario>>();
            _notificationContextMock = new Mock<NotificationContext>();
            _iMapperMock = new Mock<IMapper>();
            _servicoDeValidacaoDeFuncionariosMock = new Mock<IServicoDeValidacaoDeFuncionarios>();

            _iMapperMock.Setup(x => x.Map<Funcionario>(It.IsAny<FuncionarioDto>()))
                .Returns((FuncionarioDto source) => new Funcionario(source.Nome, source.Cpf, source.DataContratacao, source.EmpresaId));

            _armazenadorDeFuncionarios = new ArmazenadorDeFuncionarios(_repositorioBaseMockMock.Object, _notificationContextMock.Object, _servicoDeValidacaoDeFuncionariosMock.Object, _iMapperMock.Object);
        }

        [Fact]
        public void DeveCriarFuncionario()
        {
            var empresa = FuncionarioBuilder.Novo().Build();
            _funcionarioDto.Cpf = _funcionarioDto.Cpf.RemoverFormatacaoDocumento();

            empresa.AtualizarNome(_funcionarioDto.Nome);
            empresa.AtualizarCpf(_funcionarioDto.Cpf.RemoverFormatacaoDocumento());
            empresa.AtualizarDataContratacao(_funcionarioDto.DataContratacao);

            Assert.Equal(_funcionarioDto.Nome, empresa.Nome);
            Assert.Equal(_funcionarioDto.Cpf, empresa.Cpf);
            Assert.Equal(_funcionarioDto.DataContratacao, empresa.DataContratacao);
        }

        [Fact]
        public async Task DeveAdicionarFuncionario()
        {
            _funcionarioDto.Cpf = _funcionarioDto.Cpf.RemoverFormatacaoDocumento();

            await _armazenadorDeFuncionarios.Adicionar(_funcionarioDto);

            _repositorioBaseMockMock.Verify(x => x.Add(It.IsAny<Funcionario>()), Times.Once);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public async Task NaoDeveAdicionarFuncionarioComNomeInvalido(string nome)
        {
            _funcionarioDto.Nome = nome;
            bool resultadoEsperado = true;

            await _armazenadorDeFuncionarios.Adicionar(_funcionarioDto);

            _repositorioBaseMockMock.Verify(x => x.Add(It.IsAny<Funcionario>()), Times.Never);
            Assert.Equal(_notificationContextMock
                .Object
                .Notifications
                .Any(x => x.Mensagem.Equals(string.Format(Mensagens.CampoObrigatorio, Mensagens.CampoNome))), resultadoEsperado);
        }

        [Theory]
        [InlineData(155)]
        [InlineData(200)]
        public async Task NaoDeveAdicionarFuncionarioComTamanhoMaximoDeNomeExcedido(int tamanho)
        {
            bool resultadoEsperado = true;
            _funcionarioDto.Nome = _faker.Lorem.Letter(tamanho);

            await _armazenadorDeFuncionarios.Adicionar(_funcionarioDto);

            _repositorioBaseMockMock.Verify(x => x.Add(It.IsAny<Funcionario>()), Times.Never);
            Assert.Equal(_notificationContextMock
                .Object
                .Notifications
                .Any(x => x.Mensagem.Equals(string.Format(Mensagens.CampoComTamanhoMaximo, Mensagens.CampoNome, Mensagens.Tamanho150))), resultadoEsperado);
        }

        [Theory]
        [InlineData("206.349.03098")]
        [InlineData("410.454.84-59")]
        [InlineData("174.5422.30-029")]
        public async Task NaoDeveAdicionarFuncionarioComCpfInvalido(string cpf)
        {
            bool resultadoEsperado = true;
            _funcionarioDto.Cpf = cpf;

            await _armazenadorDeFuncionarios.Adicionar(_funcionarioDto);

            _repositorioBaseMockMock.Verify(x => x.Add(It.IsAny<Funcionario>()), Times.Never);
            Assert.Equal(_notificationContextMock
                .Object
                .Notifications
                .Any(x => x.Mensagem.Equals(string.Format(Mensagens.CampoInvalido, Mensagens.CampoCPF))), resultadoEsperado);
        }

        [Fact]
        public async Task NaoDeveAdicionarFuncionarioComDataContratacaoMenorOuIgualAoValorMinimo()
        {
            bool resultadoEsperado = true;
            _funcionarioDto.DataContratacao = DateTime.MinValue;

            await _armazenadorDeFuncionarios.Adicionar(_funcionarioDto);

            _repositorioBaseMockMock.Verify(x => x.Add(It.IsAny<Funcionario>()), Times.Never);
            Assert.Equal(_notificationContextMock
                .Object
                .Notifications
                .Any(x => x.Mensagem.Equals(string.Format(Mensagens.CampoDevePossuirTamanhoSuperior, Mensagens.CampoDataContratacao, _funcionarioDto.DataContratacao.ToString()))), resultadoEsperado);
        }

        [Fact]
        public async Task NaoDeveAdicionarFuncionarioSeTiverNotificacao()
        {
            await _armazenadorDeFuncionarios.Adicionar(_funcionarioDto);

            _repositorioBaseMockMock.Verify(x => x.Add(It.IsAny<Funcionario>()), Times.Never);
            Assert.True(_notificationContextMock.Object.HasNotifications);
        }
    }
}
