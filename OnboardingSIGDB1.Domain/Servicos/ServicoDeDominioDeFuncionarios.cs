using OnboardingSIGDB1.Core.Notifications;
using OnboardingSIGDB1.Domain.Entities;
using OnboardingSIGDB1.Interfaces.Data;
using OnboardingSIGDB1.Interfaces.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnboardingSIGDB1.Domain.Servicos
{
    public class ServicoDeDominioDeFuncionarios : IServicoDeDominioDeFuncionarios
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepositorioDeConsultaDeFuncionarios repositorioDeConsultaDeFuncionarios;
        private readonly NotificationContext notificationContext;
        private readonly IServicoDeValidacaoDeFuncionarios servicoDeValidacaoDeFuncionarios;

        public ServicoDeDominioDeFuncionarios(IUnitOfWork unitOfWork,
            IRepositorioDeConsultaDeFuncionarios repositorioDeConsultaDeFuncionarios,
            NotificationContext notificationContext,
            IServicoDeValidacaoDeFuncionarios servicoDeValidacaoDeFuncionarios)
        {
            this.unitOfWork = unitOfWork;
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
                    unitOfWork.Add(funcionario);
            }
            else
                notificationContext.AddNotifications(funcionario.ValidationResult);
        }

        public void Atualizar(Funcionario funcionario)
        {
            funcionario.Validar();

            if (funcionario.Valid)
                unitOfWork.Update(funcionario);
            else
                notificationContext.AddNotifications(funcionario.ValidationResult);
        }

        public void Deletar(int id)
        {
            var funcionario = repositorioDeConsultaDeFuncionarios.RecuperarPorId(id);

            if (funcionario != null)
                unitOfWork.Delete(funcionario);
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
                unitOfWork.Update(funcionarioGravado);
            }
            else
                notificationContext.AddNotification(string.Empty, "Este funcionário já possui vínculo com uma empresa");
        }
    }
}
