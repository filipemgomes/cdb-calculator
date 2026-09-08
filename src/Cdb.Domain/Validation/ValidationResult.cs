using System;
using System.Collections.Generic;

namespace Cdb.Domain.Validation
{
    public class ValidationResult
    {
        private ValidationResult(bool isValid, IReadOnlyCollection<string> errors)
        {
            IsValid = isValid;
            Errors = errors;
        }

        public bool IsValid { get; }

        public IReadOnlyCollection<string> Errors { get; }

        public static ValidationResult Success()
        {
            return new ValidationResult(true, Array.Empty<string>());
        }

        public static ValidationResult Failure(params string[] errors)
        {
            return new ValidationResult(false, errors);
        }
    }
}
