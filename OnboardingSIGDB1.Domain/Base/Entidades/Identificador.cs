using System;
using System.Collections.Generic;
using System.Text;

namespace OnboardingSIGDB1.Domain.Base.Entidades
{
    public class Identificador
    {
        public int Id { get; private set; }

        public void AtualizarId(int id) => this.Id = id;
    }
}
