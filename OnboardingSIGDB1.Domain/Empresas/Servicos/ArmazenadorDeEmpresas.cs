using AutoMapper;
using OnboardingSIGDB1.Core.Notifications;
using OnboardingSIGDB1.Core.Resources;
using OnboardingSIGDB1.Domain.Base.Interfaces;
using OnboardingSIGDB1.Domain.Empresas.Dtos;
using OnboardingSIGDB1.Domain.Empresas.Entidades;
using OnboardingSIGDB1.Domain.Empresas.Interfaces.Servicos;
using OnboardingSIGDB1.Domain.Empresas.Interfaces.Validadores;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain.Empresas.Servicos
{
    public class ArmazenadorDeEmpresas : IArmazenadorDeEmpresas
    {
        private readonly IRepositorioBase<Empresa> repositorioBase;
        private readonly NotificationContext notificationContext;
        private readonly IServicoDeValidacaoDeEmpresas servicoDeValidacaoDeEmpresas;
        private readonly IMapper iMapper;

        public ArmazenadorDeEmpresas(IRepositorioBase<Empresa> repositorioBase,
            NotificationContext notificationContext,
            IServicoDeValidacaoDeEmpresas servicoDeValidacaoDeEmpresas,
            IMapper iMapper)
        {
            this.repositorioBase = repositorioBase;
            this.notificationContext = notificationContext;
            this.servicoDeValidacaoDeEmpresas = servicoDeValidacaoDeEmpresas;
            this.iMapper = iMapper;
        }

        public async Task Adicionar(EmpresaDto dto)
        {
            if (!ValidarCnpj(dto.Cnpj)) return;

            var empresa = iMapper.Map<Empresa>(dto);

            if (!empresa.Validar())
                notificationContext.AddNotifications(empresa.ValidationResult);

            await servicoDeValidacaoDeEmpresas.Executar(empresa);

            if (notificationContext.HasNotifications) return;

            await repositorioBase.Add(empresa);
        }

        private bool ValidarCnpj(string cnpj)
        {
            string pattern = @"^(\d{2}\.\d{3}\.\d{3}\/\d{4}\-\d{2})|(\d{14})$";
            Regex regex = new Regex(pattern);

            if (!regex.IsMatch(cnpj))
            {
                notificationContext.AddNotification(string.Empty, string.Format(Mensagens.CampoInvalido, Mensagens.CampoCNPJ));
                return false;
            }
            else return true;
        }
    }
}
