using OnboardingSIGDB1.Domain.Empresas.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnboardingSIGDB1.Domain.Empresas.Interfaces.Validadores
{
    public interface IServicoDeValidacaoDeEmpresas
    {
        void Executar(Empresa empresa);
    }
}
