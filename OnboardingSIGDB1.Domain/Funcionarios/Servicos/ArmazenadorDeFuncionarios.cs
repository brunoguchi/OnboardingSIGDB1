using AutoMapper;
using OnboardingSIGDB1.Core.Notifications;
using OnboardingSIGDB1.Core.Resources;
using OnboardingSIGDB1.Domain.Base.Interfaces;
using OnboardingSIGDB1.Domain.Funcionarios.Dtos;
using OnboardingSIGDB1.Domain.Funcionarios.Entidades;
using OnboardingSIGDB1.Domain.Funcionarios.Interfaces.Repositorios;
using OnboardingSIGDB1.Domain.Funcionarios.Interfaces.Servicos;
using OnboardingSIGDB1.Domain.Funcionarios.Interfaces.Validadores;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain.Funcionarios.Servicos
{
    public class ArmazenadorDeFuncionarios : IArmazenadorDeFuncionarios
    {
        private readonly IRepositorioBase<Funcionario> repositorioBase;
        private readonly NotificationContext notificationContext;
        private readonly IServicoDeValidacaoDeFuncionarios servicoDeValidacaoDeFuncionarios;
        private readonly IMapper iMapper;

        public ArmazenadorDeFuncionarios(IRepositorioBase<Funcionario> repositorioBase,
            NotificationContext notificationContext,
            IServicoDeValidacaoDeFuncionarios servicoDeValidacaoDeFuncionarios,
            IMapper iMapper)
        {
            this.repositorioBase = repositorioBase;
            this.notificationContext = notificationContext;
            this.servicoDeValidacaoDeFuncionarios = servicoDeValidacaoDeFuncionarios;
            this.iMapper = iMapper;
        }

        public async Task Adicionar(FuncionarioDto dto)
        {
            if (!ValidarCpf(dto.Cpf)) return;

            var funcionario = iMapper.Map<Funcionario>(dto);

            if (!funcionario.Validar())
                notificationContext.AddNotifications(funcionario.ValidationResult);

            await servicoDeValidacaoDeFuncionarios.Executar(funcionario);

            if (notificationContext.HasNotifications) return;

            await repositorioBase.Add(funcionario);
        }

        private bool ValidarCpf(string cnpj)
        {
            string pattern = @"^(\d{3}\.\d{3}\.\d{3}\-\d{2})|(\d{11})$";
            Regex regex = new Regex(pattern);

            if (!regex.IsMatch(cnpj))
            {
                notificationContext.AddNotification(string.Empty, string.Format(Mensagens.CampoInvalido, Mensagens.CampoCPF));
                return false;
            }
            else return true;
        }
    }
}
