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
    public class ServicoDeValidacaoDeFuncionarios : IServicoDeValidacaoDeFuncionarios
    {
        private readonly IRepositorioDeConsultaDeFuncionarios repositorioDeConsultaDeFuncionarios;
        private readonly NotificationContext notificationContext;

        public ServicoDeValidacaoDeFuncionarios(IRepositorioDeConsultaDeFuncionarios repositorioDeConsultaDeFuncionarios,
            NotificationContext notificationContext)
        {
            this.repositorioDeConsultaDeFuncionarios = repositorioDeConsultaDeFuncionarios;
            this.notificationContext = notificationContext;
        }

        public void Executar(Funcionario funcionario)
        {
            ValidarCpf(funcionario.Cpf);
            ValidarSeFuncionarioExistentePorDocumento(funcionario.Cpf);
        }

        public void ValidarCpf(string cnpj)
        {
            string pattern = @"^(\d{3}\.\d{3}\.\d{3}\-\d{2})|(\d{11})$";
            Regex regex = new Regex(pattern);

            if (!regex.IsMatch(cnpj))
                notificationContext.AddNotification(string.Empty, "CPF inválido");
        }

        public void ValidarSeFuncionarioExistentePorDocumento(string cpf)
        {
            var funcionario = repositorioDeConsultaDeFuncionarios.RecuperarPorCpf(cpf);

            if (funcionario != null)
                notificationContext.AddNotification(string.Empty, "Já existe um funcionário cadastrado para este CPF");
        }
    }
}
