using Cdb.Domain.Calculation;
using Cdb.Domain.Simulation;
using Cdb.Domain.Tax;
using Cdb.Domain.Validation;

namespace Cdb.Domain.Tests
{
    public class CdbSimulationServiceTests
    {
        private readonly CdbSimulationService _service = new CdbSimulationService(
            new InvestmentValidator(),
            new YieldCalculator(),
            new TaxCalculator());

        [Fact]
        public void Should_ReturnErrors_WithoutCalculating_ForInvalidInput()
        {
            var result = _service.Simulate(0m, 1);

            Assert.False(result.IsValid);
            Assert.Equal(2, result.Errors.Count);
            Assert.Equal(0m, result.GrossResult);
            Assert.Equal(0m, result.NetResult);
        }

        [Fact]
        public void Should_ReturnRoundedGrossAndNetResults_ForValidSimulation()
        {
            var result = _service.Simulate(1000m, 2);

            Assert.True(result.IsValid);
            Assert.Equal(1019.53m, result.GrossResult);
            Assert.Equal(1015.14m, result.NetResult);
        }
    }
}
