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

namespace OnboardingSIGDB1.Tests.Empresas
{
    public class RemovedorDeEmpresasTests
    {
        private readonly Faker _faker;
        private readonly EmpresaDto _empresaDto;
        private readonly RemovedorDeEmpresas _removedorDeEmpresas;

        private readonly Mock<IRepositorioBase<Empresa>> repositorioBase;
        private readonly Mock<NotificationContext> notificationContext;

        public RemovedorDeEmpresasTests()
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
            _removedorDeEmpresas = new RemovedorDeEmpresas(repositorioBase.Object, notificationContext.Object);
        }

        [Fact]
        public async Task DeveDeletarEmpresa()
        {
            var empresaId = 1;
            var empresa = EmpresaBuilder.Novo().ComId(empresaId).Build();
            repositorioBase.Setup(x => x.GetById(empresaId)).ReturnsAsync(empresa);

            await _removedorDeEmpresas.Deletar(empresaId);

            repositorioBase.Verify(x => x.Remove(It.IsAny<Empresa>()), Times.Once);
        }

        [Fact]
        public async Task DeveNotificarQuandoNaoExistirEmpresaParaDelecao()
        {
            var empresaId = 10;
            Empresa empresaNaoLocalizada = null;
            repositorioBase.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(empresaNaoLocalizada);

            await _removedorDeEmpresas.Deletar(empresaId);

            Assert.True(notificationContext.Object.HasNotifications);
        }
    }
}
