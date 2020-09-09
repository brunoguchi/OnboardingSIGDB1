using AutoMapper;
using OnboardingSIGDB1.Core.Notifications;
using OnboardingSIGDB1.Core.Resources;
using OnboardingSIGDB1.Domain.Base.Interfaces;
using OnboardingSIGDB1.Domain.Funcionarios.Dtos;
using OnboardingSIGDB1.Domain.Funcionarios.Entidades;
using OnboardingSIGDB1.Domain.Funcionarios.Interfaces.Repositorios;
using OnboardingSIGDB1.Domain.Funcionarios.Interfaces.Servicos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain.Funcionarios.Servicos
{
    public class VinculadorDeFuncionarioEmpresa : IVinculadorDeFuncionarioEmpresa
    {
        private readonly IRepositorioBase<Funcionario> repositorioBase;
        private readonly NotificationContext notificationContext;

        public VinculadorDeFuncionarioEmpresa(IRepositorioBase<Funcionario> repositorioBase,
            NotificationContext notificationContext)
        {
            this.repositorioBase = repositorioBase;
            this.notificationContext = notificationContext;
        }

        public async Task VincularFuncionarioAEmpresa(FuncionarioDto funcionario)
        {
            var funcionarioGravado = await repositorioBase.GetById(funcionario.Id);

            if (funcionarioGravado == null)
                notificationContext.AddNotification(string.Empty, string.Format(Mensagens.CampoNaoLocalizado, Mensagens.CampoFuncionario));

            if (notificationContext.HasNotifications) return;

            if (ValidarSeFuncionarioTemVinculoComEmpesa(funcionarioGravado))
                notificationContext.AddNotification(string.Empty, Mensagens.FuncionarioJaVinculadoAEmpresa);

            if (notificationContext.HasNotifications) return;

            funcionarioGravado.AtualizarEmpresaId(funcionario.EmpresaId);
        }

        private bool ValidarSeFuncionarioTemVinculoComEmpesa(Funcionario funcionario)
        {
            if (funcionario.EmpresaId == null || funcionario.EmpresaId == 0)
                return false;
            else return true;
        }
    }
}
