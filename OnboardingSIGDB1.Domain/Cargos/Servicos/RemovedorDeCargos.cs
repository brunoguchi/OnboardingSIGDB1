using AutoMapper;
using OnboardingSIGDB1.Core.Notifications;
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
        private readonly IConsultasDeCargos consultasDeCargos;
        private readonly NotificationContext notificationContext;
        private readonly IMapper _iMapper;

        public RemovedorDeCargos(IRepositorioBase<Cargo> repositorioBase,
            IConsultasDeCargos consultasDeCargos,
            NotificationContext notificationContext,
            IMapper iMapper)
        {
            this.repositorioBase = repositorioBase;
            this.consultasDeCargos = consultasDeCargos;
            this.notificationContext = notificationContext;
            this._iMapper = iMapper;
        }

        public async Task Deletar(int id)
        {
            var cargoDto = await consultasDeCargos.RecuperarPorId(id);
            var cargo =  _iMapper.Map<Cargo>(cargoDto);

            if (cargo != null)
                await repositorioBase.Remove(cargo);
            else
                notificationContext.AddNotification(string.Empty, "Cargo não localizado");
        }
    }
}
