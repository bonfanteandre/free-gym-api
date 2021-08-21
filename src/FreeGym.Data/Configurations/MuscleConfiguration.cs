using FreeGym.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreeGym.Data.Configurations
{
    public class MuscleConfiguration : IEntityTypeConfiguration<Muscle>
    {
        public void Configure(EntityTypeBuilder<Muscle> builder)
        {
            builder.HasKey(m => m.Id);

            builder
                .Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}