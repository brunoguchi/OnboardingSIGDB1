using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnboardingSIGDB1.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace OnboardingSIGDB1.Data.Mapeamentos
{
    public class FuncionarioMapping : IEntityTypeConfiguration<Funcionario>
    {
        public void Configure(EntityTypeBuilder<Funcionario> builder)
        {
            builder.Property(x => x.Nome)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(x => x.Cpf)
                .HasMaxLength(11)
                .IsFixedLength()
                .IsRequired();

            builder.Property(x => x.DataContratacao).IsRequired();
            builder.Property(x => x.EmpresaId).IsRequired();

            builder.HasOne(x => x.Empresa)
                .WithMany()
                .HasForeignKey(x => x.EmpresaId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
