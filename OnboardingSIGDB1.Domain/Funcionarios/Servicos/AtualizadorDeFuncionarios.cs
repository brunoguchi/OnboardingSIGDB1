using AutoMapper;
using OnboardingSIGDB1.Core.Notifications;
using OnboardingSIGDB1.Core.Resources;
using OnboardingSIGDB1.Domain.Base.Interfaces;
using OnboardingSIGDB1.Domain.Funcionarios.Dtos;
using OnboardingSIGDB1.Domain.Funcionarios.Entidades;
using OnboardingSIGDB1.Domain.Funcionarios.Interfaces.Repositorios;
using OnboardingSIGDB1.Domain.Funcionarios.Interfaces.Servicos;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain.Funcionarios.Servicos
{
    public class AtualizadorDeFuncionarios : IAtualizadorDeFuncionarios
    {
        private readonly IRepositorioBase<Funcionario> repositorioBase;
        private readonly NotificationContext notificationContext;

        public AtualizadorDeFuncionarios(IRepositorioBase<Funcionario> repositorioBase,
            NotificationContext notificationContext)
        {
            this.repositorioBase = repositorioBase;
            this.notificationContext = notificationContext;
        }

        public async Task Atualizar(FuncionarioDto dto)
        {
            var funcionarioGravado = await repositorioBase.GetById(dto.Id);

            ValidarFuncionario(funcionarioGravado);

            if (notificationContext.HasNotifications) return;

            funcionarioGravado.AtualizarNome(dto.Nome);
            funcionarioGravado.AtualizarCpf(dto.Cpf);
            funcionarioGravado.AtualizarDataContratacao(dto.DataContratacao);

            if (!funcionarioGravado.Validar())
                notificationContext.AddNotifications(funcionarioGravado.ValidationResult);
        }

        private void ValidarFuncionario(Funcionario funcionario)
        {
            if (funcionario == null)
                notificationContext.AddNotification(string.Empty, string.Format(Mensagens.CampoNaoLocalizado, Mensagens.CampoFuncionario));
        }
    }
}
