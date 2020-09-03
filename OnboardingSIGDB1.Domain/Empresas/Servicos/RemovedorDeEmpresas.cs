using AutoMapper;
using OnboardingSIGDB1.Core.Notifications;
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
        private readonly IConsultasDeEmpresas consultasDeEmpresas;
        private readonly IMapper _iMapper;

        public RemovedorDeEmpresas(IRepositorioBase<Empresa> repositorioBase,
            NotificationContext notificationContext,
            IConsultasDeEmpresas consultasDeEmpresas,
            IMapper _iMapper)
        {
            this.repositorioBase = repositorioBase;
            this.notificationContext = notificationContext;
            this.consultasDeEmpresas = consultasDeEmpresas;
            this._iMapper = _iMapper;
        }

        public async Task Deletar(int id)
        {
            var empresaDto = await consultasDeEmpresas.RecuperarPorId(id);
            var empresa = _iMapper.Map<Empresa>(empresaDto);

            if (empresa != null)
                await repositorioBase.Remove(empresa);
            else
                notificationContext.AddNotification(string.Empty, "Empresa não localizada");
        }
    }
}
