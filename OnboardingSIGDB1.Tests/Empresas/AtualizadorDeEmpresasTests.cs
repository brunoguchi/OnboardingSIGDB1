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
            var cargo = EmpresaBuilder.Novo().ComId(1).Build();
            repositorioBase.Setup(x => x.GetById(_empresaDto.Id)).ReturnsAsync(cargo);
            _empresaDto.Cnpj = _empresaDto.Cnpj.RemoverFormatacaoDocumento();

            await _atualizadorDeEmpresas.Atualizar(_empresaDto);

            Assert.Equal(_empresaDto.Nome, cargo.Nome);
            Assert.Equal(_empresaDto.Cnpj, cargo.Cnpj);
            Assert.Equal(_empresaDto.DataFundacao, cargo.DataFundacao);
        }

        [Fact]
        public async Task DeveNotificarQuandoNaoExistirEmpresaParaEdicao()
        {
            Empresa empresaNaoLocalizada = null;
            repositorioBase.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(empresaNaoLocalizada);

            await _atualizadorDeEmpresas.Atualizar(_empresaDto);

            Assert.True(notificationContext.Object.HasNotifications);
        }
    }
}
