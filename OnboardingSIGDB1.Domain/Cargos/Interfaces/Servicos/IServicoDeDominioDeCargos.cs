using OnboardingSIGDB1.Domain.Cargos.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnboardingSIGDB1.Domain.Cargos.Interfaces.Servicos
{
    public interface IServicoDeDominioDeCargos
    {
        IEnumerable<Cargo> ListarTodos();
        Cargo RecuperarPorId(int id);
        void Adicionar(Cargo cargo);
        void Atualizar(Cargo cargo);
        void Deletar(int id);
    }
}
