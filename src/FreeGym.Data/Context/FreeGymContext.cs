using System.Reflection;
using FreeGym.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FreeGym.Data.Context
{
    public class FreeGymContext : DbContext
    {
        public DbSet<Muscle> Muscles { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<ExerciseMuscle> ExerciseMuscles { get; set; }

        public FreeGymContext(DbContextOptions<FreeGymContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}