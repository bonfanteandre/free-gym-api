using System.Collections.Generic;

namespace FreeGym.Core.Entities
{
    public class Exercise : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<ExerciseMuscle> ExerciseMuscles { get; set; }
    }
}