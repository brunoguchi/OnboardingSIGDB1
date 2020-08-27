using OnboardingSIGDB1.Core.Notifications;
using OnboardingSIGDB1.Domain.Entities;
using OnboardingSIGDB1.Interfaces.Data;
using OnboardingSIGDB1.Interfaces.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain.Servicos
{
    public class ServicoDeDominioDeEmpresas : IServicoDeDominioDeEmpresas
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepositorioDeConsultaDeEmpresas repositorioDeConsultaDeEmpresas;
        private readonly NotificationContext notificationContext;
        private readonly IServicoDeValidacaoDeEmpresas servicoDeValidacaoDeEmpresas;

        public ServicoDeDominioDeEmpresas(IUnitOfWork unitOfWork,
            IRepositorioDeConsultaDeEmpresas repositorioDeConsultaDeEmpresas,
            NotificationContext notificationContext,
            IServicoDeValidacaoDeEmpresas servicoDeValidacaoDeEmpresas)
        {
            this.unitOfWork = unitOfWork;
            this.repositorioDeConsultaDeEmpresas = repositorioDeConsultaDeEmpresas;
            this.notificationContext = notificationContext;
            this.servicoDeValidacaoDeEmpresas = servicoDeValidacaoDeEmpresas;
        }

        public void Adicionar(Empresa empresa)
        {
            empresa.Validar();
            servicoDeValidacaoDeEmpresas.Executar(empresa);

            if (empresa.Valid)
            {
                if (!notificationContext.HasNotifications)
                    unitOfWork.Add(empresa);
            }
            else
                notificationContext.AddNotifications(empresa.ValidationResult);
        }

        public void Atualizar(Empresa empresa)
        {
            empresa.Validar();

            if (empresa.Valid)
                unitOfWork.Update(empresa);
            else
                notificationContext.AddNotifications(empresa.ValidationResult);
        }

        public void Deletar(int id)
        {
            var empresa = repositorioDeConsultaDeEmpresas.RecuperarPorId(id);

            if (empresa != null)
                unitOfWork.Delete(empresa);
            else
                notificationContext.AddNotification(string.Empty, "Empresa não localizada");
        }

        public IEnumerable<Empresa> ListarTodas()
        {
            return repositorioDeConsultaDeEmpresas.ListarTodas();
        }

        public IEnumerable<Empresa> PesquisarPorFiltro(EmpresaFiltro filtro)
        {
            return repositorioDeConsultaDeEmpresas.RecuperarPorFiltro(filtro);
        }

        public Empresa RecuperarPorId(int id)
        {
            return repositorioDeConsultaDeEmpresas.RecuperarPorId(id);
        }
    }
}
