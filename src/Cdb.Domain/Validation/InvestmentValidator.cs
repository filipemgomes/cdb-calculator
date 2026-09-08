using System.Collections.Generic;

namespace Cdb.Domain.Validation
{
    public class InvestmentValidator : IInvestmentValidator
    {
        public ValidationResult Validate(decimal initialAmount, int termInMonths)
        {
            var errors = new List<string>();

            if (initialAmount <= 0)
            {
                errors.Add("O valor inicial deve ser um valor monetário positivo.");
            }

            if (termInMonths < 2)
            {
                errors.Add("O prazo deve ser um número inteiro de meses maior que 1.");
            }

            return errors.Count == 0
                ? ValidationResult.Success()
                : ValidationResult.Failure(errors.ToArray());
        }
    }
}
