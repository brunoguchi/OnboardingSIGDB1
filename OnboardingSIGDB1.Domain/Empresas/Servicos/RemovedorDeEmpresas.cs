using AutoMapper;
using OnboardingSIGDB1.Core.Notifications;
using OnboardingSIGDB1.Core.Resources;
using OnboardingSIGDB1.Domain.Base.Interfaces;
using OnboardingSIGDB1.Domain.Empresas.Entidades;
using OnboardingSIGDB1.Domain.Empresas.Interfaces.Repositorios;
using OnboardingSIGDB1.Domain.Empresas.Interfaces.Servicos;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain.Empresas.Servicos
{
    public class RemovedorDeEmpresas : IRemovedorDeEmpresas
    {
        private readonly IRepositorioBase<Empresa> repositorioBase;
        private readonly NotificationContext notificationContext;

        public RemovedorDeEmpresas(IRepositorioBase<Empresa> repositorioBase,
            NotificationContext notificationContext)
        {
            this.repositorioBase = repositorioBase;
            this.notificationContext = notificationContext;
        }

        public async Task Deletar(int id)
        {
            var empresa = await repositorioBase.GetById(id);

            ValidarEmpresa(empresa);

            if (notificationContext.HasNotifications) return;

            await repositorioBase.Remove(empresa);
        }

        private void ValidarEmpresa(Empresa empresa)
        {
            if (empresa == null)
                notificationContext.AddNotification(string.Empty, string.Format(Mensagens.CampoNaoLocalizado, Mensagens.CampoEmpresa));
        }
    }
}
