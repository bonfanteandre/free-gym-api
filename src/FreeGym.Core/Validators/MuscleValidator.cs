using FluentValidation;
using FreeGym.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeGym.Core.Validators
{
    public class MuscleValidator : AbstractValidator<Muscle>
    {
        public MuscleValidator()
        {
            RuleFor(muscle => muscle.Name)
                .NotEmpty().WithMessage("Nome é um campo obrigatório")
                .Length(3, 50).WithMessage("Nome deve possuir entre {MinLength} e {MaxLength} caracteres");
        }
    }
}
