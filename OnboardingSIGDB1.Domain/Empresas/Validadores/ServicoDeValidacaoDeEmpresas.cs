using OnboardingSIGDB1.Core.Notifications;
using OnboardingSIGDB1.Domain.Empresas.Entidades;
using OnboardingSIGDB1.Domain.Empresas.Interfaces.Repositorios;
using OnboardingSIGDB1.Domain.Empresas.Interfaces.Validadores;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain.Servicos
{
    public class ServicoDeValidacaoDeEmpresas : IServicoDeValidacaoDeEmpresas
    {
        private readonly IConsultasDeEmpresas consultasDeEmpresas;
        private readonly NotificationContext notificationContext;

        public ServicoDeValidacaoDeEmpresas(IConsultasDeEmpresas consultasDeEmpresas,
            NotificationContext notificationContext)
        {
            this.consultasDeEmpresas = consultasDeEmpresas;
            this.notificationContext = notificationContext;
        }

        public async Task Executar(Empresa empresa)
        {
            await ValidarSeEmpresaExistentePorDocumento(empresa.Cnpj);
            await ValidarCnpj(empresa.Cnpj);
        }

        private async Task ValidarCnpj(string cnpj)
        {
            string pattern = @"^(\d{2}\.\d{3}\.\d{3}\/\d{4}\-\d{2})|(\d{14})$";
            Regex regex = new Regex(pattern);

            if (!regex.IsMatch(cnpj))
                notificationContext.AddNotification(string.Empty, "CNPJ inválido");
        }

        private async Task ValidarSeEmpresaExistentePorDocumento(string cnpj)
        {
            var empresa = await consultasDeEmpresas.RecuperarPorCnpj(cnpj);

            if (empresa != null)
                notificationContext.AddNotification(string.Empty, "Já existe uma empresa cadastrada para este CNPJ");
        }
    }
}
