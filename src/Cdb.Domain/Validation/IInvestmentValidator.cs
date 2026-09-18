namespace Cdb.Domain.Validation
{
    public interface IInvestmentValidator
    {
        ValidationResult Validate(decimal initialAmount, int termInMonths);
    }
}
