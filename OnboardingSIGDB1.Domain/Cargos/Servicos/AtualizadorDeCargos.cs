using OnboardingSIGDB1.Core.Notifications;
using OnboardingSIGDB1.Domain.Base.Interfaces;
using OnboardingSIGDB1.Domain.Cargos.Entidades;
using OnboardingSIGDB1.Domain.Cargos.Interfaces.Servicos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain.Cargos.Servicos
{
    public class AtualizadorDeCargos : IAtualizadorDeCargos
    {
        private readonly IRepositorioBase<Cargo> repositorioBase;
        private readonly NotificationContext notificationContext;

        public AtualizadorDeCargos(IRepositorioBase<Cargo> repositorioBase,
            NotificationContext notificationContext)
        {
            this.repositorioBase = repositorioBase;
            this.notificationContext = notificationContext;
        }

        public async Task Atualizar(Cargo cargo)
        {
            cargo.Validar();

            if (cargo.Valid)
                await repositorioBase.Update(cargo);
            else
                notificationContext.AddNotifications(cargo.ValidationResult);
        }
    }
}
