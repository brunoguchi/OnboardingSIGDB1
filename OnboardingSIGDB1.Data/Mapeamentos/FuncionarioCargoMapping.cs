using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnboardingSIGDB1.Domain.Entities;

namespace OnboardingSIGDB1.Data.Mapeamentos
{
    public class FuncionarioCargoMapping : IEntityTypeConfiguration<FuncionarioCargo>
    {
        public void Configure(EntityTypeBuilder<FuncionarioCargo> builder)
        {
            builder.HasKey(x => new { x.CargoId, x.FuncionarioId });
        }
    }
}
