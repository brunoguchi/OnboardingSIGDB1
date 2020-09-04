using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnboardingSIGDB1.Domain.Funcionarios.Entidades;

namespace OnboardingSIGDB1.Data.Funcionarios.Mappings
{
    public class FuncionarioCargoMapping : IEntityTypeConfiguration<FuncionarioCargo>
    {
        public void Configure(EntityTypeBuilder<FuncionarioCargo> builder)
        {
            builder.HasKey(x => new { x.CargoId, x.FuncionarioId });

            builder.HasOne(x => x.Cargo)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Funcionario)
                .WithMany(x => x.FuncionariosCargos)
                .HasForeignKey(x => x.FuncionarioId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
