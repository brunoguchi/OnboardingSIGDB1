using OnboardingSIGDB1.Core.Notifications;
using OnboardingSIGDB1.Core.Resources;
using OnboardingSIGDB1.Domain.Base.Interfaces;
using OnboardingSIGDB1.Domain.Empresas.Dtos;
using OnboardingSIGDB1.Domain.Empresas.Entidades;
using OnboardingSIGDB1.Domain.Empresas.Interfaces.Servicos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain.Empresas.Servicos
{
    public class AtualizadorDeEmpresas : IAtualizadorDeEmpresas
    {
        private readonly IRepositorioBase<Empresa> repositorioBase;
        private readonly NotificationContext notificationContext;

        public AtualizadorDeEmpresas(IRepositorioBase<Empresa> repositorioBase,
            NotificationContext notificationContext)
        {
            this.repositorioBase = repositorioBase;
            this.notificationContext = notificationContext;
        }

        public async Task Atualizar(EmpresaDto dto)
        {
            var empresaGravada = await repositorioBase.GetById(dto.Id);

            ValidarEmpresa(empresaGravada);

            if (notificationContext.HasNotifications) return;

            empresaGravada.AtualizarNome(dto.Nome);
            empresaGravada.AtualizarCnpj(dto.Cnpj);
            empresaGravada.AtualizarDataFundacao(dto.DataFundacao);

            if (!empresaGravada.Validar())
                notificationContext.AddNotifications(empresaGravada.ValidationResult);
        }

        private void ValidarEmpresa(Empresa empresa)
        {
            if (empresa == null)
                notificationContext.AddNotification(string.Empty, string.Format(Mensagens.CampoNaoLocalizado, Mensagens.CampoEmpresa));
        }
    }
}
