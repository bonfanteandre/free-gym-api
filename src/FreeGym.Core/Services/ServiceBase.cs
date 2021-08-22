using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeGym.Core.Services
{
    public abstract class ServiceBase<T>
    {
        private readonly ICollection<string> _errors;
        public ICollection<string> Errors => new List<string>(_errors);
        public bool IsValid => _errors.Count == 0;

        public ServiceBase()
        {
            _errors = new List<string>();
        }

        protected void AddError(string error)
        {
            if (string.IsNullOrEmpty(error))
            {
                throw new ArgumentNullException(nameof(error));
            }

            _errors.Add(error);
        }

        protected void AddErrors(ICollection<string> errors)
        {
            if (errors == null)
            {
                throw new ArgumentNullException(nameof(errors));
            }

            foreach (var error in errors)
            {
                if (string.IsNullOrEmpty(error))
                {
                    throw new ArgumentNullException(nameof(error));
                }

                _errors.Add(error);
            }
        }

        protected bool ExecuteValidation(AbstractValidator<T> validator, T entity)
        {
            var result = validator.Validate(entity);

            if (result.IsValid)
            {
                return true;
            }

            var errors = result.Errors.Select(e => e.ErrorMessage).ToList();
            AddErrors(errors);

            return false;
        }
    }
}
