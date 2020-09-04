using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnboardingSIGDB1.Domain.Empresas.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnboardingSIGDB1.Data.Empresas.Mappings
{
    public class EmpresaMapping : IEntityTypeConfiguration<Empresa>
    {
        public void Configure(EntityTypeBuilder<Empresa> builder)
        {
            builder.Property(x => x.Nome)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(x => x.Cnpj)
                .HasMaxLength(14)
                .IsFixedLength()
                .IsRequired();

            builder.Property(x => x.DataFundacao).IsRequired();

            builder.Ignore(x => x.Invalid);
            builder.Ignore(x => x.Valid);
            builder.Ignore(x => x.ValidationResult);
        }
    }
}
