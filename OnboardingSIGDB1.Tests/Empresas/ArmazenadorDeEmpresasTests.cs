using AutoMapper;
using Bogus;
using Bogus.Extensions.Brazil;
using Moq;
using OnboardingSIGDB1.Core.Notifications;
using OnboardingSIGDB1.Domain.Base.Interfaces;
using OnboardingSIGDB1.Domain.Empresas.Dtos;
using OnboardingSIGDB1.Domain.Empresas.Entidades;
using OnboardingSIGDB1.Domain.Empresas.Interfaces.Validadores;
using OnboardingSIGDB1.Domain.Empresas.Servicos;
using OnboardingSIGDB1.Tests.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using OnboardingSIGDB1.Core.Extensions;
using System.Threading.Tasks;
using System.Linq;
using OnboardingSIGDB1.Core.Resources;

namespace OnboardingSIGDB1.Tests.Empresas
{
    public class ArmazenadorDeEmpresasTests
    {
        private readonly Faker _faker;
        private readonly EmpresaDto _empresaDto;
        private readonly ArmazenadorDeEmpresas _armazenadorDeEmpresas;

        private readonly Mock<IRepositorioBase<Empresa>> repositorioBase;
        private readonly Mock<NotificationContext> notificationContext;
        private readonly Mock<IMapper> iMapper;
        private readonly Mock<IServicoDeValidacaoDeEmpresas> servicoDeValidacaoDeEmpresas;

        public ArmazenadorDeEmpresasTests()
        {
            _faker = new Faker();
            _empresaDto = new EmpresaDto
            {
                Nome = _faker.Company.CompanyName(),
                Cnpj = _faker.Company.Cnpj(),
                DataFundacao = DateTime.Now
            };

            repositorioBase = new Mock<IRepositorioBase<Empresa>>();
            notificationContext = new Mock<NotificationContext>();
            iMapper = new Mock<IMapper>();
            servicoDeValidacaoDeEmpresas = new Mock<IServicoDeValidacaoDeEmpresas>();

            iMapper.Setup(x => x.Map<Empresa>(It.IsAny<EmpresaDto>()))
                .Returns((EmpresaDto source) => new Empresa(source.Nome, source.Cnpj, source.DataFundacao));

            _armazenadorDeEmpresas = new ArmazenadorDeEmpresas(repositorioBase.Object, notificationContext.Object, servicoDeValidacaoDeEmpresas.Object, iMapper.Object);
        }

        [Fact]
        public void DeveCriarEmpresa()
        {
            var empresa = EmpresaBuilder.Novo().Build();
            _empresaDto.Cnpj = _empresaDto.Cnpj.RemoverFormatacaoDocumento();

            empresa.AtualizarNome(_empresaDto.Nome);
            empresa.AtualizarCnpj(_empresaDto.Cnpj.RemoverFormatacaoDocumento());
            empresa.AtualizarDataFundacao(_empresaDto.DataFundacao);

            Assert.Equal(_empresaDto.Nome, empresa.Nome);
            Assert.Equal(_empresaDto.Cnpj, empresa.Cnpj);
            Assert.Equal(_empresaDto.DataFundacao, empresa.DataFundacao);
        }

        [Fact]
        public async Task DeveAdicionarEmpresa()
        {
            _empresaDto.Cnpj = _empresaDto.Cnpj.RemoverFormatacaoDocumento();

            await _armazenadorDeEmpresas.Adicionar(_empresaDto);

            repositorioBase.Verify(x => x.Add(It.IsAny<Empresa>()), Times.Once);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public async Task NaoDeveAdicionarEmpresaComNomeInvalido(string nome)
        {
            _empresaDto.Nome = nome;
            bool resultadoEsperado = true;

            await _armazenadorDeEmpresas.Adicionar(_empresaDto);

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
            bool resultadoEsperado = true;
            _empresaDto.Nome = _faker.Lorem.Letter(tamanho);

            await _armazenadorDeEmpresas.Adicionar(_empresaDto);

            repositorioBase.Verify(x => x.Add(It.IsAny<Empresa>()), Times.Never);
            Assert.Equal(notificationContext
                .Object
                .Notifications
                .Any(x => x.Mensagem.Equals(string.Format(Mensagens.CampoComTamanhoMaximo, Mensagens.CampoNome, Mensagens.Tamanho150))), resultadoEsperado);
        }

        [Theory]
        [InlineData("04.501.2790/0001-48")]
        [InlineData("024.5011.279/0001-48")]
        [InlineData("04.501.27901-4")]
        public async Task NaoDeveAdicionarEmpresaComCnpjInvalido(string cnpj)
        {
            bool resultadoEsperado = true;
            _empresaDto.Cnpj = cnpj;

            await _armazenadorDeEmpresas.Adicionar(_empresaDto);

            repositorioBase.Verify(x => x.Add(It.IsAny<Empresa>()), Times.Never);
            Assert.Equal(notificationContext
                .Object
                .Notifications
                .Any(x => x.Mensagem.Equals(string.Format(Mensagens.CampoInvalido, Mensagens.CampoCNPJ))), resultadoEsperado);
        }

        [Fact]
        public async Task NaoDeveAdicionarEmpresaComDataFundacaoMenorOuIgualAoValorMinimo()
        {
            bool resultadoEsperado = true;
            _empresaDto.DataFundacao = DateTime.MinValue;

            await _armazenadorDeEmpresas.Adicionar(_empresaDto);

            repositorioBase.Verify(x => x.Add(It.IsAny<Empresa>()), Times.Never);
            Assert.Equal(notificationContext
                .Object
                .Notifications
                .Any(x => x.Mensagem.Equals(string.Format(Mensagens.CampoDevePossuirTamanhoSuperior, Mensagens.CampoDataFundacao, _empresaDto.DataFundacao.ToString()))), resultadoEsperado);
        }

        [Fact]
        public async Task NaoDeveAdicionarEmpresaSeTiverNotificacao()
        {
            await _armazenadorDeEmpresas.Adicionar(_empresaDto);

            repositorioBase.Verify(x => x.Add(It.IsAny<Empresa>()), Times.Never);
            Assert.True(notificationContext.Object.HasNotifications);
        }
    }
}
