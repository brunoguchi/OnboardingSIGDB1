using Bogus;
using Bogus.Extensions.Brazil;
using Moq;
using OnboardingSIGDB1.Core.Notifications;
using OnboardingSIGDB1.Domain.Base.Interfaces;
using OnboardingSIGDB1.Domain.Empresas.Dtos;
using OnboardingSIGDB1.Domain.Empresas.Entidades;
using OnboardingSIGDB1.Domain.Empresas.Servicos;
using OnboardingSIGDB1.Tests.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using OnboardingSIGDB1.Core.Extensions;
using System.Linq;
using OnboardingSIGDB1.Core.Resources;

namespace OnboardingSIGDB1.Tests.Empresas
{
    public class AtualizadorDeEmpresasTests
    {
        private readonly Faker _faker;
        private readonly EmpresaDto _empresaDto;
        private readonly AtualizadorDeEmpresas _atualizadorDeEmpresas;

        private readonly Mock<IRepositorioBase<Empresa>> repositorioBase;
        private readonly Mock<NotificationContext> notificationContext;

        public AtualizadorDeEmpresasTests()
        {
            _faker = new Faker();
            _empresaDto = new EmpresaDto
            {
                Id = _faker.Random.Int(min: 1, max: 100),
                Nome = _faker.Company.CompanyName(),
                Cnpj = _faker.Company.Cnpj(),
                DataFundacao = DateTime.Now
            };

            repositorioBase = new Mock<IRepositorioBase<Empresa>>();
            notificationContext = new Mock<NotificationContext>();
            _atualizadorDeEmpresas = new AtualizadorDeEmpresas(repositorioBase.Object, notificationContext.Object);
        }

        [Fact]
        public async Task DeveEditarEmpresa()
        {
            var empresa = EmpresaBuilder.Novo().ComId(_empresaDto.Id).Build();
            repositorioBase.Setup(x => x.GetById(_empresaDto.Id)).ReturnsAsync(empresa);
            _empresaDto.Cnpj = _empresaDto.Cnpj.RemoverFormatacaoDocumento();

            await _atualizadorDeEmpresas.Atualizar(_empresaDto);

            Assert.Equal(_empresaDto.Nome, empresa.Nome);
            Assert.Equal(_empresaDto.Cnpj, empresa.Cnpj);
            Assert.Equal(_empresaDto.DataFundacao, empresa.DataFundacao);
        }

        [Fact]
        public async Task DeveNotificarQuandoNaoExistirEmpresaParaEdicao()
        {
            Empresa empresaNaoLocalizada = null;
            repositorioBase.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(empresaNaoLocalizada);

            await _atualizadorDeEmpresas.Atualizar(_empresaDto);

            Assert.True(notificationContext.Object.HasNotifications);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public async Task NaoDeveAdicionarEmpresaComNomeInvalido(string nome)
        {
            var empresa = EmpresaBuilder.Novo().ComId(_empresaDto.Id).Build();
            repositorioBase.Setup(x => x.GetById(_empresaDto.Id)).ReturnsAsync(empresa);
            _empresaDto.Nome = nome;
            bool resultadoEsperado = true;

            await _atualizadorDeEmpresas.Atualizar(_empresaDto);

            repositorioBase.Verify(x => x.Add(It.IsAny<Empresa>()), Times.Never);
            Assert.Equal(notificationContext
                .Object
                .Notifications
                .Any(x => x.Mensagem.Equals(string.Format(Mensagens.CampoObrigatorio, Mensagens.CampoNome))), resultadoEsperado);
        }

        [Theory]
        [InlineData(155)]
        [InlineData(200)]
        public async Task NaoDeveAdicionarEmpresaComTamanhoMaximoDeNomeExcedido(int tamanho)
        {
            var empresa = EmpresaBuilder.Novo().ComId(_empresaDto.Id).Build();
            repositorioBase.Setup(x => x.GetById(_empresaDto.Id)).ReturnsAsync(empresa);
            bool resultadoEsperado = true;
            _empresaDto.Nome = _faker.Lorem.Letter(tamanho);

            await _atualizadorDeEmpresas.Atualizar(_empresaDto);

            repositorioBase.Verify(x => x.Add(It.IsAny<Empresa>()), Times.Never);
            Assert.Equal(notificationContext
                .Object
                .Notifications
                .Any(x => x.Mensagem.Equals(string.Format(Mensagens.CampoComTamanhoMaximo, Mensagens.CampoNome, Mensagens.Tamanho150))), resultadoEsperado);
        }

        [Fact]
        public async Task NaoDeveAdicionarEmpresaComDataFundacaoMenorOuIgualAoValorMinimo()
        {
            var empresa = EmpresaBuilder.Novo().ComId(_empresaDto.Id).Build();
            repositorioBase.Setup(x => x.GetById(_empresaDto.Id)).ReturnsAsync(empresa);
            bool resultadoEsperado = true;
            _empresaDto.DataFundacao = DateTime.MinValue;

            await _atualizadorDeEmpresas.Atualizar(_empresaDto);

            repositorioBase.Verify(x => x.Add(It.IsAny<Empresa>()), Times.Never);
            Assert.Equal(notificationContext
                .Object
                .Notifications
                .Any(x => x.Mensagem.Equals(string.Format(Mensagens.CampoDevePossuirTamanhoSuperior, Mensagens.CampoDataFundacao, _empresaDto.DataFundacao.ToString()))), resultadoEsperado);
        }

        [Fact]
        public async Task NaoDeveAdicionarEmpresaSeTiverNotificacao()
        {
            await _atualizadorDeEmpresas.Atualizar(_empresaDto);

            repositorioBase.Verify(x => x.Add(It.IsAny<Empresa>()), Times.Never);
            Assert.True(notificationContext.Object.HasNotifications);
        }

        [Theory]
        [InlineData("04.501.2790/0001-48")]
        [InlineData("024.5011.279/0001-48")]
        public async Task NaoDeveAdicionarEmpresaComCnpjDeTamanhoExcedido(string cnpj)
        {
            var empresa = EmpresaBuilder.Novo().ComId(_empresaDto.Id).Build();
            repositorioBase.Setup(x => x.GetById(_empresaDto.Id)).ReturnsAsync(empresa);
            bool resultadoEsperado = true;
            _empresaDto.Cnpj = cnpj;

            await _atualizadorDeEmpresas.Atualizar(_empresaDto);

            repositorioBase.Verify(x => x.Add(It.IsAny<Empresa>()), Times.Never);
            Assert.Equal(notificationContext
                .Object
                .Notifications
                .Any(x => x.Mensagem.Equals(string.Format(Mensagens.CampoComTamanhoMaximo, Mensagens.CampoCNPJ, Mensagens.Tamanho14))), resultadoEsperado);
        }

        [Theory]
        [InlineData("04.501.27901-4")]
        [InlineData("4.1.2790/0001-48")]
        public async Task NaoDeveAdicionarEmpresaComCnpjDeTamanhoAbaixoDoPermitido(string cnpj)
        {
            var empresa = EmpresaBuilder.Novo().ComId(_empresaDto.Id).Build();
            repositorioBase.Setup(x => x.GetById(_empresaDto.Id)).ReturnsAsync(empresa);
            bool resultadoEsperado = true;
            _empresaDto.Cnpj = cnpj;

            await _atualizadorDeEmpresas.Atualizar(_empresaDto);

            repositorioBase.Verify(x => x.Add(It.IsAny<Empresa>()), Times.Never);
            Assert.Equal(notificationContext
                .Object
                .Notifications
                .Any(x => x.Mensagem.Equals(string.Format(Mensagens.CampoComTamanhoFixo, Mensagens.CampoCNPJ, Mensagens.Tamanho14))), resultadoEsperado);
        }
    }
}
