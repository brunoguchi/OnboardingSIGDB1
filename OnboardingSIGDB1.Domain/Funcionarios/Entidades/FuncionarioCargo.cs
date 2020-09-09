using OnboardingSIGDB1.Domain.Cargos.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace OnboardingSIGDB1.Domain.Funcionarios.Entidades
{
    public class FuncionarioCargo
    {
        public int FuncionarioId { get; set; }
        public virtual Funcionario Funcionario { get; set; }

        public int CargoId { get; set; }
        public virtual Cargo Cargo { get; set; }

        public DateTime DataDeVinculo { get; set; }

        protected FuncionarioCargo() { }

        public FuncionarioCargo(Funcionario funcionario, Cargo cargo, DateTime DataDeVinculo)
        {
            this.Funcionario = funcionario;
            this.Cargo = cargo;
            this.DataDeVinculo = DataDeVinculo;

            this.FuncionarioId = funcionario.Id;
            this.CargoId = cargo.Id;
        }
    }
}
