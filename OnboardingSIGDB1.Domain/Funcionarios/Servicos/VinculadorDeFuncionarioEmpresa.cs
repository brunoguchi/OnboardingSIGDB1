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
        private readonly IConsultaDeFuncionarios consultaDeFuncionarios;
        private readonly NotificationContext notificationContext;
        private readonly IMapper iMapper;

        public VinculadorDeFuncionarioEmpresa(IRepositorioBase<Funcionario> repositorioBase,
            IConsultaDeFuncionarios consultaDeFuncionarios,
            NotificationContext notificationContext,
            IMapper iMapper)
        {
            this.repositorioBase = repositorioBase;
            this.consultaDeFuncionarios = consultaDeFuncionarios;
            this.notificationContext = notificationContext;
            this.iMapper = iMapper;
        }

        public async Task VincularFuncionarioAEmpresa(FuncionarioDto funcionario)
        {
            var funcionarioGravado = await repositorioBase.GetById(funcionario.Id);

            if (!ValidarSeFuncionarioTemVinculoComEmpesa(funcionarioGravado))
                funcionarioGravado.AtualizarEmpresaId(funcionario.EmpresaId);
            else
                notificationContext.AddNotification(string.Empty, Mensagens.FuncionarioJaVinculadoAEmpresa);
        }

        private bool ValidarSeFuncionarioTemVinculoComEmpesa(Funcionario funcionario)
        {
            if (funcionario.EmpresaId == null || funcionario.EmpresaId == 0)
                return false;
            else return true;
        }
    }
}
