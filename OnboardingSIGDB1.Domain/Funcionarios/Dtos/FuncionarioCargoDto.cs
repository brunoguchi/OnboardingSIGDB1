﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain.Funcionarios.Dtos
{
    public class FuncionarioCargoDto
    {
        public int FuncionarioId { get; set; }
        public int CargoId { get; set; }
        public DateTime DataDeVinculo { get; set; }
    }
}