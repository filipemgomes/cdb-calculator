using Cdb.Domain.Validation;

namespace Cdb.Domain.Tests.Validation
{
    public class InvestmentValidatorTests
    {
        private readonly InvestmentValidator _validator = new InvestmentValidator();

        [Theory]
        [InlineData(1000, 2)]
        public void Should_ReturnSuccess_ForValidInput(decimal initialAmount, int termInMonths)
        {
            var result = _validator.Validate(initialAmount, termInMonths);

            Assert.True(result.IsValid);
            Assert.Empty(result.Errors);
        }

        [Theory]
        [InlineData(0, 2)]
        public void Should_ReturnError_ForNonPositiveInitialValue(decimal initialAmount, int termInMonths)
        {
            var result = _validator.Validate(initialAmount, termInMonths);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.Contains("valor inicial"));
        }

        [Theory]
        [InlineData(1000, 1)]
        public void Should_ReturnError_WhenTermIsLessThanTwo(decimal initialAmount, int termInMonths)
        {
            var result = _validator.Validate(initialAmount, termInMonths);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.Contains("prazo"));
        }
    }
}
