using Cdb.Domain.Tax;

namespace Cdb.Domain.Tests.Tax
{
    public class TaxCalculatorTests
    {
        private readonly TaxCalculator _calculator = new TaxCalculator();

        [Theory]
        [InlineData(6, 0.225)]
        [InlineData(7, 0.20)]
        [InlineData(12, 0.20)]
        [InlineData(13, 0.175)]
        [InlineData(24, 0.175)]
        [InlineData(25, 0.15)]
        public void Should_SelectCorrectTaxRate_ByTermRange(int termInMonths, decimal expectedRate)
        {
            const decimal initialAmount = 1000m;
            const decimal finalAmount = 1100m;
            var earnings = finalAmount - initialAmount;

            var tax = _calculator.CalculateTax(finalAmount, initialAmount, termInMonths);

            Assert.Equal(earnings * expectedRate, tax);
        }

        [Fact]
        public void Should_ApplyTax_OnlyToEarnings()
        {
            var tax = _calculator.CalculateTax(1100m, 1000m, 6);

            Assert.Equal(22.5m, tax);
        }
    }
}
