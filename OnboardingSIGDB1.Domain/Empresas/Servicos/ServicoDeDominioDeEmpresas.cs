using OnboardingSIGDB1.Core.Notifications;
using OnboardingSIGDB1.Domain.Base.Interfaces;
using OnboardingSIGDB1.Domain.Empresas.Entidades;
using OnboardingSIGDB1.Domain.Empresas.Interfaces.Repositorios;
using OnboardingSIGDB1.Domain.Empresas.Interfaces.Servicos;
using OnboardingSIGDB1.Domain.Empresas.Interfaces.Validadores;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain.Servicos
{
    public class ServicoDeDominioDeEmpresas : IServicoDeDominioDeEmpresas
    {
        private readonly IRepositorioBase<Empresa> repositorioBase;
        private readonly IRepositorioDeConsultaDeEmpresas repositorioDeConsultaDeEmpresas;
        private readonly NotificationContext notificationContext;
        private readonly IServicoDeValidacaoDeEmpresas servicoDeValidacaoDeEmpresas;

        public ServicoDeDominioDeEmpresas(IRepositorioBase<Empresa> repositorioBase,
            IRepositorioDeConsultaDeEmpresas repositorioDeConsultaDeEmpresas,
            NotificationContext notificationContext,
            IServicoDeValidacaoDeEmpresas servicoDeValidacaoDeEmpresas)
        {
            this.repositorioBase = repositorioBase;
            this.repositorioDeConsultaDeEmpresas = repositorioDeConsultaDeEmpresas;
            this.notificationContext = notificationContext;
            this.servicoDeValidacaoDeEmpresas = servicoDeValidacaoDeEmpresas;
        }

        public void Adicionar(Empresa empresa)
        {
            empresa.Validar();
            servicoDeValidacaoDeEmpresas.Executar(empresa);

            if (empresa.Valid)
            {
                if (!notificationContext.HasNotifications)
                    repositorioBase.Add(empresa);
            }
            else
                notificationContext.AddNotifications(empresa.ValidationResult);
        }

        public void Atualizar(Empresa empresa)
        {
            empresa.Validar();

            if (empresa.Valid)
                repositorioBase.Update(empresa);
            else
                notificationContext.AddNotifications(empresa.ValidationResult);
        }

        public void Deletar(int id)
        {
            var empresa = repositorioDeConsultaDeEmpresas.RecuperarPorId(id);

            if (empresa != null)
                repositorioBase.Remove(empresa);
            else
                notificationContext.AddNotification(string.Empty, "Empresa não localizada");
        }

        public IEnumerable<Empresa> ListarTodas()
        {
            return repositorioDeConsultaDeEmpresas.ListarTodas();
        }

        public IEnumerable<Empresa> PesquisarPorFiltro(EmpresaFiltro filtro)
        {
            return repositorioDeConsultaDeEmpresas.RecuperarPorFiltro(filtro);
        }

        public Empresa RecuperarPorId(int id)
        {
            return repositorioDeConsultaDeEmpresas.RecuperarPorId(id);
        }
    }
}
