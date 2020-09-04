using AutoMapper;
using OnboardingSIGDB1.Core.Notifications;
using OnboardingSIGDB1.Core.Resources;
using OnboardingSIGDB1.Domain.Base.Interfaces;
using OnboardingSIGDB1.Domain.Cargos.Entidades;
using OnboardingSIGDB1.Domain.Cargos.Interfaces.Consultas;
using OnboardingSIGDB1.Domain.Cargos.Interfaces.Servicos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain.Cargos.Servicos
{
    public class RemovedorDeCargos : IRemovedorDeCargos
    {
        private readonly IRepositorioBase<Cargo> repositorioBase;
        private readonly NotificationContext notificationContext;

        public RemovedorDeCargos(IRepositorioBase<Cargo> repositorioBase,
            NotificationContext notificationContext)
        {
            this.repositorioBase = repositorioBase;
            this.notificationContext = notificationContext;
        }

        public async Task Deletar(int id)
        {
            var cargo = await repositorioBase.GetById(id);

            ValidarCargo(cargo);

            if (notificationContext.HasNotifications) return;

            await repositorioBase.Remove(cargo);
        }

        private void ValidarCargo(Cargo cargo)
        {
            if (cargo == null)
                notificationContext.AddNotification(string.Empty, string.Format(Mensagens.CampoNaoLocalizado, Mensagens.CampoCargo));
        }
    }
}
