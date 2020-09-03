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
    public class ArmazenadorDeCargos : IArmazenadorDeCargos
    {
        private readonly IRepositorioBase<Cargo> repositorioBase;
        private readonly NotificationContext notificationContext;

        public ArmazenadorDeCargos(IRepositorioBase<Cargo> repositorioBase,
            NotificationContext notificationContext)
        {
            this.repositorioBase = repositorioBase;
            this.notificationContext = notificationContext;
        }

        public async Task Adicionar(Cargo cargo)
        {
            cargo.Validar();

            if (cargo.Valid)
            {
                if (!notificationContext.HasNotifications)
                    await repositorioBase.Add(cargo);
            }
            else
                notificationContext.AddNotifications(cargo.ValidationResult);
        }
    }
}
