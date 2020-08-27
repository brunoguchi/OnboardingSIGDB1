using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnboardingSIGDB1.Domain.Entities;

namespace OnboardingSIGDB1.Data.Mapeamentos
{
    public class CargoMapping : IEntityTypeConfiguration<Cargo>
    {
        public void Configure(EntityTypeBuilder<Cargo> builder)
        {
            builder.Property(x => x.Descricao)
                .HasMaxLength(250)
                .IsRequired();

            builder.Ignore(x => x.Invalid);
            builder.Ignore(x => x.Valid);
            builder.Ignore(x => x.ValidationResult);
        }
    }
}
