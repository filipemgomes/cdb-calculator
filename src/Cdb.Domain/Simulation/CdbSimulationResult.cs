using System;
using System.Collections.Generic;

namespace Cdb.Domain.Simulation
{
    public class CdbSimulationResult
    {
        private CdbSimulationResult(bool isValid, IReadOnlyCollection<string> errors, decimal grossResult, decimal netResult)
        {
            IsValid = isValid;
            Errors = errors;
            GrossResult = grossResult;
            NetResult = netResult;
        }

        public bool IsValid { get; }

        public IReadOnlyCollection<string> Errors { get; }

        public decimal GrossResult { get; }

        public decimal NetResult { get; }

        public static CdbSimulationResult Invalid(IReadOnlyCollection<string> errors)
        {
            return new CdbSimulationResult(false, errors, 0m, 0m);
        }

        public static CdbSimulationResult Valid(decimal grossResult, decimal netResult)
        {
            return new CdbSimulationResult(true, Array.Empty<string>(), grossResult, netResult);
        }
    }
}
