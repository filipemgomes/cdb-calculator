using System;
using Cdb.Domain.Calculation;
using Cdb.Domain.Tax;
using Cdb.Domain.Validation;

namespace Cdb.Domain.Simulation
{
    public class CdbSimulationService : ICdbSimulationService
    {
        private readonly IInvestmentValidator _validator;
        private readonly IYieldCalculator _yieldCalculator;
        private readonly ITaxCalculator _taxCalculator;

        public CdbSimulationService(IInvestmentValidator validator, IYieldCalculator yieldCalculator, ITaxCalculator taxCalculator)
        {
            _validator = validator;
            _yieldCalculator = yieldCalculator;
            _taxCalculator = taxCalculator;
        }

        public CdbSimulationResult Simulate(decimal initialAmount, int termInMonths)
        {
            var validation = _validator.Validate(initialAmount, termInMonths);
            if (!validation.IsValid)
            {
                return CdbSimulationResult.Invalid(validation.Errors);
            }

            var finalAmount = _yieldCalculator.CalculateFinalValue(initialAmount, termInMonths);
            var tax = _taxCalculator.CalculateTax(finalAmount, initialAmount, termInMonths);
            var netAmount = finalAmount - tax;

            var grossResult = Math.Round(finalAmount, 2, MidpointRounding.AwayFromZero);
            var netResult = Math.Round(netAmount, 2, MidpointRounding.AwayFromZero);

            return CdbSimulationResult.Valid(grossResult, netResult);
        }
    }
}
