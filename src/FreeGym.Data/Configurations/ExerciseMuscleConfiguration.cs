using FreeGym.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreeGym.Data.Configurations
{
    public class ExerciseMuscleConfiguration : IEntityTypeConfiguration<ExerciseMuscle>
    {
        public void Configure(EntityTypeBuilder<ExerciseMuscle> builder)
        {
            builder.HasKey(em => new { em.ExerciseId, em.MuscleId });

            builder
                .HasOne(em => em.Exercise)
                .WithMany(e => e.ExerciseMuscles)
                .HasForeignKey(em => em.ExerciseId);

            builder
                .HasOne(em => em.Muscle)
                .WithMany(m => m.ExerciseMuscles)
                .HasForeignKey(em => em.MuscleId);
        }
    }
}