using AutoMapper;
using OnboardingSIGDB1.Core.Notifications;
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
    public class ArmazenadorDeCargos : IArmazenadorDeCargos
    {
        private readonly IRepositorioBase<Cargo> repositorioBase;
        private readonly NotificationContext notificationContext;
        private readonly IMapper iMapper;

        public ArmazenadorDeCargos(IRepositorioBase<Cargo> repositorioBase,
            NotificationContext notificationContext,
            IMapper iMapper)
        {
            this.repositorioBase = repositorioBase;
            this.notificationContext = notificationContext;
            this.iMapper = iMapper;
        }

        public async Task Adicionar(CargoDto dto)
        {
            var cargo = iMapper.Map<Cargo>(dto);

            if (!cargo.Validar())
                notificationContext.AddNotifications(cargo.ValidationResult);

            if (notificationContext.HasNotifications) return;

            await repositorioBase.Add(cargo);
        }
    }
}
