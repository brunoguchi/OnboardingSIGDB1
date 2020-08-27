using OnboardingSIGDB1.Core.Notifications;
using OnboardingSIGDB1.Domain.Entities;
using OnboardingSIGDB1.Interfaces.Data;
using OnboardingSIGDB1.Interfaces.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace OnboardingSIGDB1.Domain.Servicos
{
    public class ServicoDeValidacaoDeEmpresas : IServicoDeValidacaoDeEmpresas
    {
        private readonly IRepositorioDeConsultaDeEmpresas repositorioDeConsultaDeEmpresas;
        private readonly NotificationContext notificationContext;

        public ServicoDeValidacaoDeEmpresas(IRepositorioDeConsultaDeEmpresas repositorioDeConsultaDeEmpresas,
            NotificationContext notificationContext)
        {
            this.repositorioDeConsultaDeEmpresas = repositorioDeConsultaDeEmpresas;
            this.notificationContext = notificationContext;
        }

        public void Executar(Empresa empresa)
        {
            ValidarSeEmpresaExistentePorDocumento(empresa.Cnpj);
            ValidarCnpj(empresa.Cnpj);
        }

        public void ValidarCnpj(string cnpj)
        {
            string pattern = @"^(\d{2}\.\d{3}\.\d{3}\/\d{4}\-\d{2})|(\d{14})$";
            Regex regex = new Regex(pattern);

            if (!regex.IsMatch(cnpj))
                notificationContext.AddNotification(string.Empty, "CNPJ inválido");
        }

        public void ValidarSeEmpresaExistentePorDocumento(string cnpj)
        {
            var empresa = repositorioDeConsultaDeEmpresas.RecuperarPorCnpj(cnpj);

            if (empresa != null)
                notificationContext.AddNotification(string.Empty, "Já existe uma empresa cadastrada para este CNPJ");
        }
    }
}
