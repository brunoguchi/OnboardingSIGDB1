using AutoMapper;
using OnboardingSIGDB1.Core.Notifications;
using OnboardingSIGDB1.Core.Resources;
using OnboardingSIGDB1.Domain.Base.Interfaces;
using OnboardingSIGDB1.Domain.Cargos.Entidades;
using OnboardingSIGDB1.Domain.Cargos.Interfaces.Consultas;
using OnboardingSIGDB1.Domain.Funcionarios.Dtos;
using OnboardingSIGDB1.Domain.Funcionarios.Entidades;
using OnboardingSIGDB1.Domain.Funcionarios.Interfaces.Repositorios;
using OnboardingSIGDB1.Domain.Funcionarios.Interfaces.Servicos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain.Funcionarios.Servicos
{
    public class VinculadorDeFuncionarioCargo : IVinculadorDeFuncionarioCargo
    {
        private readonly IRepositorioDeFuncionarios repositorioDeFuncionarios;
        private readonly IRepositorioBase<Cargo> repositorioCargoBase;
        private readonly NotificationContext notificationContext;
        private readonly IMapper iMapper;

        public VinculadorDeFuncionarioCargo(IRepositorioDeFuncionarios repositorioDeFuncionarios,
            IRepositorioBase<Cargo> repositorioCargoBase,
            NotificationContext notificationContext,
            IMapper iMapper)
        {
            this.repositorioDeFuncionarios = repositorioDeFuncionarios;
            this.notificationContext = notificationContext;
            this.iMapper = iMapper;
            this.repositorioCargoBase = repositorioCargoBase;
        }

        public async Task VincularFuncionarioAoCargo(FuncionarioCargoDto funcionarioCargo)
        {
            var funcionarioGravado = await repositorioDeFuncionarios.RecuperarPorIdComCargos(funcionarioCargo.FuncionarioId);
            var cargoGravado = await repositorioCargoBase.GetById(funcionarioCargo.CargoId);

            if (funcionarioGravado == null || cargoGravado == null)
                notificationContext.AddNotification(string.Empty, Mensagens.DadosInválidosParaVinculoDeFuncionarioAoCargo);

            if (!ValidarSeFuncionarioTemVinculoComEmpesa(funcionarioGravado))
                notificationContext.AddNotification(string.Empty, Mensagens.VincularFuncionarioAUmaEmpresa);

            if (!ValidarCargoJaAtribuido(funcionarioGravado, funcionarioCargo.CargoId))
                notificationContext.AddNotification(string.Empty, Mensagens.FuncionarioJaVinculadoAoACargo);

            funcionarioGravado.AdicionarCargo(cargoGravado, funcionarioCargo.DataDeVinculo);
        }

        private bool ValidarSeFuncionarioTemVinculoComEmpesa(Funcionario funcionario)
        {
            if (funcionario.EmpresaId == null || funcionario.EmpresaId == 0)
                return false;
            else return true;
        }

        private bool ValidarCargoJaAtribuido(Funcionario funcionario, int cargoId)
        {
            if (funcionario.FuncionariosCargos == null) return false;

            return funcionario.FuncionariosCargos.Where(x => x.CargoId == cargoId).Count() == 0;
        }
    }
}
