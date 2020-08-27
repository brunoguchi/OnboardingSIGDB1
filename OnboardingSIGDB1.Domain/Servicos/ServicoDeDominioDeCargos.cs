using OnboardingSIGDB1.Core.Notifications;
using OnboardingSIGDB1.Domain.Entities;
using OnboardingSIGDB1.Interfaces.Data;
using OnboardingSIGDB1.Interfaces.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnboardingSIGDB1.Domain.Servicos
{
    public class ServicoDeDominioDeCargos : IServicoDeDominioDeCargos
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepositorioDeConsultaDeCargos repositorioDeConsultaDeCargos;
        private readonly NotificationContext notificationContext;

        public ServicoDeDominioDeCargos(IUnitOfWork unitOfWork,
            IRepositorioDeConsultaDeCargos repositorioDeConsultaDeCargos,
            NotificationContext notificationContext)
        {
            this.unitOfWork = unitOfWork;
            this.repositorioDeConsultaDeCargos = repositorioDeConsultaDeCargos;
            this.notificationContext = notificationContext;
        }

        public void Adicionar(Cargo cargo)
        {
            cargo.Validar();

            if (cargo.Valid)
            {
                if (!notificationContext.HasNotifications)
                    unitOfWork.Add(cargo);
            }
            else
                notificationContext.AddNotifications(cargo.ValidationResult);
        }

        public void Atualizar(Cargo cargo)
        {
            cargo.Validar();

            if (cargo.Valid)
                unitOfWork.Update(cargo);
            else
                notificationContext.AddNotifications(cargo.ValidationResult);
        }

        public void Deletar(int id)
        {
            var cargo = repositorioDeConsultaDeCargos.RecuperarPorId(id);

            if (cargo != null)
                unitOfWork.Delete(cargo);
            else
                notificationContext.AddNotification(string.Empty, "Cargo não localizado");
        }

        public IEnumerable<Cargo> ListarTodos()
        {
            return repositorioDeConsultaDeCargos.ListarTodos();
        }

        public Cargo RecuperarPorId(int id)
        {
            return repositorioDeConsultaDeCargos.RecuperarPorId(id);
        }
    }
}
