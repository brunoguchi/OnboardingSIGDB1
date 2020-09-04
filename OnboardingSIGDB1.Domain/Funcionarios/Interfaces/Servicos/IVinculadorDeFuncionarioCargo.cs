﻿using OnboardingSIGDB1.Domain.Funcionarios.Dtos;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain.Funcionarios.Interfaces.Servicos
{
    public interface IVinculadorDeFuncionarioCargo
    {
        Task VincularFuncionarioAoCargo(FuncionarioCargoDto funcionarioCargo);
    }
}
