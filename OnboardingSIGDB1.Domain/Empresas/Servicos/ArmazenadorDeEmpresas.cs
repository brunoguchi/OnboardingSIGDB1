using OnboardingSIGDB1.Core.Notifications;
using OnboardingSIGDB1.Domain.Base.Interfaces;
using OnboardingSIGDB1.Domain.Empresas.Entidades;
using OnboardingSIGDB1.Domain.Empresas.Interfaces.Servicos;
using OnboardingSIGDB1.Domain.Empresas.Interfaces.Validadores;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain.Empresas.Servicos
{
    public class ArmazenadorDeEmpresas : IArmazenadorDeEmpresas
    {
        private readonly IRepositorioBase<Empresa> repositorioBase;
        private readonly NotificationContext notificationContext;
        private readonly IServicoDeValidacaoDeEmpresas servicoDeValidacaoDeEmpresas;

        public ArmazenadorDeEmpresas(IRepositorioBase<Empresa> repositorioBase,
            NotificationContext notificationContext,
            IServicoDeValidacaoDeEmpresas servicoDeValidacaoDeEmpresas)
        {
            this.repositorioBase = repositorioBase;
            this.notificationContext = notificationContext;
            this.servicoDeValidacaoDeEmpresas = servicoDeValidacaoDeEmpresas;
        }

        public async Task Adicionar(Empresa empresa)
        {
            empresa.Validar();
            await servicoDeValidacaoDeEmpresas.Executar(empresa);

            if (empresa.Valid)
            {
                if (!notificationContext.HasNotifications)
                    await repositorioBase.Add(empresa);
            }
            else
                notificationContext.AddNotifications(empresa.ValidationResult);
        }
    }
}
