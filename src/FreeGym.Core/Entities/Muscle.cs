using System.Collections.Generic;

namespace FreeGym.Core.Entities
{
    public class Muscle : EntityBase
    {
        public string Name { get; set; }

        public ICollection<ExerciseMuscle> ExerciseMuscles { get; set; }
    }
}