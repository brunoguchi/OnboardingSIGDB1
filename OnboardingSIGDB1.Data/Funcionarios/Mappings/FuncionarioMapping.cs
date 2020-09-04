using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnboardingSIGDB1.Domain.Funcionarios.Entidades;

namespace OnboardingSIGDB1.Data.Funcionarios.Mappings
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
            builder.Property(x => x.EmpresaId).IsRequired(false);

            builder.HasOne(x => x.Empresa)
                .WithMany()
                .HasForeignKey(x => x.EmpresaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Ignore(x => x.FuncionariosCargos);
            builder.Ignore(x => x.Invalid);
            builder.Ignore(x => x.Valid);
            builder.Ignore(x => x.ValidationResult);
        }
    }
}
