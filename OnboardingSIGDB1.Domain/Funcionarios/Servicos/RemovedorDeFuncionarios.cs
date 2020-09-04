using AutoMapper;
using OnboardingSIGDB1.Core.Notifications;
using OnboardingSIGDB1.Core.Resources;
using OnboardingSIGDB1.Domain.Base.Interfaces;
using OnboardingSIGDB1.Domain.Funcionarios.Entidades;
using OnboardingSIGDB1.Domain.Funcionarios.Interfaces.Repositorios;
using OnboardingSIGDB1.Domain.Funcionarios.Interfaces.Servicos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain.Funcionarios.Servicos
{
    public class RemovedorDeFuncionarios : IRemovedorDeFuncionarios
    {
        private readonly IRepositorioBase<Funcionario> repositorioBase;
        private readonly NotificationContext notificationContext;

        public RemovedorDeFuncionarios(IRepositorioBase<Funcionario> repositorioBase,
            NotificationContext notificationContext)
        {
            this.repositorioBase = repositorioBase;
            this.notificationContext = notificationContext;
        }

        public async Task Deletar(int id)
        {
            var funcionario = await repositorioBase.GetById(id);

            ValidarFuncionario(funcionario);

            if (notificationContext.HasNotifications) return;

            await repositorioBase.Remove(funcionario);
        }

        private void ValidarFuncionario(Funcionario funcionario)
        {
            if (funcionario == null)
                notificationContext.AddNotification(string.Empty, string.Format(Mensagens.CampoNaoLocalizado, Mensagens.CampoFuncionario));
        }
    }
}
