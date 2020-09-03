using OnboardingSIGDB1.Core.Notifications;
using OnboardingSIGDB1.Domain.Base.Interfaces;
using OnboardingSIGDB1.Domain.Empresas.Entidades;
using OnboardingSIGDB1.Domain.Empresas.Interfaces.Servicos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain.Empresas.Servicos
{
    public class AtualizadorDeEmpresas : IAtualizadorDeEmpresas
    {
        private readonly IRepositorioBase<Empresa> repositorioBase;
        private readonly NotificationContext notificationContext;

        public AtualizadorDeEmpresas(IRepositorioBase<Empresa> repositorioBase,
            NotificationContext notificationContext)
        {
            this.repositorioBase = repositorioBase;
            this.notificationContext = notificationContext;
        }

        public async Task Atualizar(Empresa empresa)
        {
            empresa.Validar();

            if (empresa.Valid)
                await repositorioBase.Update(empresa);
            else
                notificationContext.AddNotifications(empresa.ValidationResult);
        }
    }
}
