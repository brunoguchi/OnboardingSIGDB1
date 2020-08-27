using OnboardingSIGDB1.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnboardingSIGDB1.Interfaces.Domain
{
    public interface IServicoDeValidacaoDeEmpresas
    {
        void Executar(Empresa empresa);
        void ValidarCnpj(string cnpj);
        void ValidarSeEmpresaExistentePorDocumento(string cnpj);
    }
}
