using Bogus;
using Bogus.Extensions.Brazil;
using Moq;
using OnboardingSIGDB1.Core.Notifications;
using OnboardingSIGDB1.Domain.Empresas.Dtos;
using OnboardingSIGDB1.Domain.Empresas.Entidades;
using OnboardingSIGDB1.Domain.Empresas.Interfaces.Repositorios;
using OnboardingSIGDB1.Domain.Servicos;
using OnboardingSIGDB1.Tests.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OnboardingSIGDB1.Tests.Empresas
{
    public class ServicoDeValidacaoDeEmpresasTests
    {
        private readonly Mock<IConsultasDeEmpresas> consultasDeEmpresasMock;
        private readonly Mock<NotificationContext> notificationContextMock;
        private readonly ServicoDeValidacaoDeEmpresas servicoDeValidacaoDeEmpresas;
        private readonly Faker _faker;

        public ServicoDeValidacaoDeEmpresasTests()
        {
            this.consultasDeEmpresasMock = new Mock<IConsultasDeEmpresas>();
            this.notificationContextMock = new Mock<NotificationContext>();
            _faker = new Faker();

            servicoDeValidacaoDeEmpresas = new ServicoDeValidacaoDeEmpresas(consultasDeEmpresasMock.Object, notificationContextMock.Object);
        }

        [Fact]
        public async Task DeveNotificarQuandoEmpresaJaEstiverCadastradaComMesmoCNPJ()
        {
            var empresaDto = new EmpresaDto { Id = _faker.Random.Int(), Cnpj = _faker.Company.Cnpj(), DataFundacao = DateTime.Now };
            var empresa = EmpresaBuilder.Novo().Build();
            empresa.AtualizarCnpj(empresaDto.Cnpj);
            consultasDeEmpresasMock.Setup(x => x.RecuperarPorCnpj(It.IsAny<string>())).ReturnsAsync(empresaDto);

            await servicoDeValidacaoDeEmpresas.Executar(empresa);

            Assert.True(notificationContextMock.Object.HasNotifications);
        }
    }
}
