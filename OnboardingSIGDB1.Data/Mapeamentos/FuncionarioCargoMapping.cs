using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnboardingSIGDB1.Domain.Entities;
using System.Security.Cryptography.X509Certificates;

namespace OnboardingSIGDB1.Data.Mapeamentos
{
    public class FuncionarioCargoMapping : IEntityTypeConfiguration<FuncionarioCargo>
    {
        public void Configure(EntityTypeBuilder<FuncionarioCargo> builder)
        {
            builder.HasKey(x => new { x.CargoId, x.FuncionarioId });

            builder.HasOne(x => x.Cargo)
                .WithMany(x => x.FuncionariosCargos)
                .HasForeignKey(x => x.CargoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Funcionario)
                .WithMany(x => x.FuncionariosCargos)
                .HasForeignKey(x => x.FuncionarioId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
