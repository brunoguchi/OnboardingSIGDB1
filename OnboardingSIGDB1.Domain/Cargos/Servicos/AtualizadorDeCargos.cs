using OnboardingSIGDB1.Core.Notifications;
using OnboardingSIGDB1.Core.Resources;
using OnboardingSIGDB1.Domain.Base.Interfaces;
using OnboardingSIGDB1.Domain.Cargos.Dtos;
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

        public async Task Atualizar(CargoDto dto)
        {
            var cargoGravado = await repositorioBase.GetById(dto.Id);

            ValidarCargo(cargoGravado);

            if (notificationContext.HasNotifications) return;

            cargoGravado.AtualizarDescricao(dto.Descricao);

            if (!cargoGravado.Validar())
                notificationContext.AddNotifications(cargoGravado.ValidationResult);
        }

        private void ValidarCargo(Cargo cargo)
        {
            if (cargo == null)
                notificationContext.AddNotification(string.Empty, string.Format(Mensagens.CampoNaoLocalizado, Mensagens.CampoCargo));
        }
    }
}
