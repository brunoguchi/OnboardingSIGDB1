using OnboardingSIGDB1.Core.Notifications;
using OnboardingSIGDB1.Domain.Base.Interfaces;
using OnboardingSIGDB1.Domain.Funcionarios.Entidades;
using OnboardingSIGDB1.Domain.Funcionarios.Interfaces.Repositorios;
using OnboardingSIGDB1.Domain.Funcionarios.Interfaces.Servicos;
using OnboardingSIGDB1.Domain.Funcionarios.Interfaces.Validadores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnboardingSIGDB1.Domain.Servicos
{
    public class ServicoDeDominioDeFuncionarios : IServicoDeDominioDeFuncionarios
    {
        private readonly IRepositorioBase<Funcionario> repositorioBase;
        private readonly IRepositorioDeConsultaDeFuncionarios repositorioDeConsultaDeFuncionarios;
        private readonly NotificationContext notificationContext;
        private readonly IServicoDeValidacaoDeFuncionarios servicoDeValidacaoDeFuncionarios;

        public ServicoDeDominioDeFuncionarios(IRepositorioBase<Funcionario> repositorioBase,
            IRepositorioDeConsultaDeFuncionarios repositorioDeConsultaDeFuncionarios,
            NotificationContext notificationContext,
            IServicoDeValidacaoDeFuncionarios servicoDeValidacaoDeFuncionarios)
        {
            this.repositorioBase = repositorioBase;
            this.repositorioDeConsultaDeFuncionarios = repositorioDeConsultaDeFuncionarios;
            this.notificationContext = notificationContext;
            this.servicoDeValidacaoDeFuncionarios = servicoDeValidacaoDeFuncionarios;
        }

        public void Adicionar(Funcionario funcionario)
        {
            funcionario.Validar();
            servicoDeValidacaoDeFuncionarios.Executar(funcionario);

            if (funcionario.Valid)
            {
                if (!notificationContext.HasNotifications)
                    repositorioBase.Add(funcionario);
            }
            else
                notificationContext.AddNotifications(funcionario.ValidationResult);
        }

        public void Atualizar(Funcionario funcionario)
        {
            funcionario.Validar();

            if (funcionario.Valid)
                repositorioBase.Update(funcionario);
            else
                notificationContext.AddNotifications(funcionario.ValidationResult);
        }

        public void Deletar(int id)
        {
            var funcionario = repositorioDeConsultaDeFuncionarios.RecuperarPorId(id);

            if (funcionario != null)
                repositorioBase.Remove(funcionario);
            else
                notificationContext.AddNotification(string.Empty, "Funcionário não localizado");
        }

        public IEnumerable<Funcionario> ListarTodos()
        {
            return repositorioDeConsultaDeFuncionarios.ListarTodos();
        }

        public IEnumerable<Funcionario> PesquisarPorFiltro(FuncionarioFiltro filtro)
        {
            return repositorioDeConsultaDeFuncionarios.RecuperarPorFiltro(filtro);
        }

        public Funcionario RecuperarPorId(int id)
        {
            return repositorioDeConsultaDeFuncionarios.RecuperarPorId(id);
        }

        public void VincularFuncionarioAEmpresa(Funcionario funcionario)
        {
            var funcionarioGravado = repositorioDeConsultaDeFuncionarios.RecuperarPorId(funcionario.Id);

            if (funcionarioGravado.EmpresaId == null || funcionarioGravado.EmpresaId == 0)
            {
                funcionarioGravado.EmpresaId = funcionario.EmpresaId;
                repositorioBase.Update(funcionarioGravado);
            }
            else
                notificationContext.AddNotification(string.Empty, "Este funcionário já possui vínculo com uma empresa");
        }

        public void VincularFuncionarioAoCargo(FuncionarioCargo funcionarioCargo)
        {
            //var funcionarioGravado = repositorioDeConsultaDeFuncionarios.RecuperarPorIdComTodosOsCargos(funcionarioCargo.FuncionarioId);
            //var cargoGravado = repositorioDeConsultaDeCargos.RecuperarPorId(funcionarioCargo.CargoId);

            //if (funcionarioGravado != null && cargoGravado != null)
            //{
            //    if (funcionarioGravado.EmpresaId != null && funcionarioGravado.EmpresaId != 0)
            //    {
            //        var possuiCargo = funcionarioGravado.FuncionariosCargos.Where(x => x.CargoId == funcionarioCargo.CargoId).Count() > 0;

            //        if (!possuiCargo)
            //            repositorioBase.Add(funcionarioCargo);
            //        else
            //            notificationContext.AddNotification(string.Empty, "Este funcionário já está vinculado a este cargo");
            //    }
            //    else
            //        notificationContext.AddNotification(string.Empty, "Necessário vincular o funcionário a uma empresa");
            //}
            //else
            //{
            //    notificationContext.AddNotification(string.Empty, "Dados inválidos para vincular funcionário a um cargo");
            //}
        }
    }
}
