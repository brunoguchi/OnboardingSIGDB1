using OnboardingSIGDB1.Core.Notifications;
using OnboardingSIGDB1.Core.Resources;
using OnboardingSIGDB1.Domain.Funcionarios.Entidades;
using OnboardingSIGDB1.Domain.Funcionarios.Interfaces.Repositorios;
using OnboardingSIGDB1.Domain.Funcionarios.Interfaces.Validadores;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain.Servicos
{
    public class ServicoDeValidacaoDeFuncionarios : IServicoDeValidacaoDeFuncionarios
    {
        private readonly IConsultaDeFuncionarios consultaDeFuncionarios;
        private readonly NotificationContext notificationContext;

        public ServicoDeValidacaoDeFuncionarios(IConsultaDeFuncionarios consultaDeFuncionarios,
            NotificationContext notificationContext)
        {
            this.consultaDeFuncionarios = consultaDeFuncionarios;
            this.notificationContext = notificationContext;
        }

        public async Task Executar(Funcionario funcionario)
        {
            await ValidarSeFuncionarioExistentePorDocumento(funcionario.Cpf);
        }

        private async Task ValidarSeFuncionarioExistentePorDocumento(string cpf)
        {
            var funcionario = await consultaDeFuncionarios.RecuperarPorCpf(cpf);

            if (funcionario != null)
                notificationContext.AddNotification(string.Empty, string.Format(Mensagens.CampoJaCadastradoParaEsteValor, Mensagens.CampoFuncionario, cpf));
        }
    }
}
