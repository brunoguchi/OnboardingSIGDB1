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
using System;
using System.Collections.Generic;
using System.Text;

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
        private readonly Mock<IServicoDeValidacaoDeEmpresas> _servicoDeValidacaoDeEmpresas;

        public ArmazenadorDeEmpresasTests()
        {
            _faker = new Faker();
            _empresaDto = new EmpresaDto
            {
                Nome = _faker.Company.CompanyName(),
                Cnpj = _faker.Company.Cnpj(),
                DataFundacao = Convert.ToDateTime(_faker.Date)
            };

            repositorioBase = new Mock<IRepositorioBase<Empresa>>();
            notificationContext = new Mock<NotificationContext>();
            iMapper = new Mock<IMapper>();

            iMapper.Setup(x => x.Map<Empresa>(It.IsAny<EmpresaDto>()))
                .Returns((EmpresaDto source) => new Empresa() { });

            _armazenadorDeEmpresas = new ArmazenadorDeEmpresas(repositorioBase.Object, notificationContext.Object, _servicoDeValidacaoDeEmpresas.Object, iMapper.Object);
        }
    }
}
