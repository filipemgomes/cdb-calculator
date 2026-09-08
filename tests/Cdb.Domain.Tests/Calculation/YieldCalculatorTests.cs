using Cdb.Domain.Calculation;

namespace Cdb.Domain.Tests.Calculation
{
    public class YieldCalculatorTests
    {
        private readonly YieldCalculator _calculator = new YieldCalculator();

        [Fact]
        public void Should_CalculateFinalValue_WithTwoMonths()
        {
            var result = _calculator.CalculateFinalValue(1000m, 2);

            Assert.Equal(1019.5344784m, result);
        }
    }
}
