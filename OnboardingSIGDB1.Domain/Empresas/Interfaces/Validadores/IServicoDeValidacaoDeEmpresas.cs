﻿using OnboardingSIGDB1.Domain.Empresas.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain.Empresas.Interfaces.Validadores
{
    public interface IServicoDeValidacaoDeEmpresas
    {
        Task Executar(Empresa empresa);
    }
}
