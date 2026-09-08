namespace Cdb.Domain.Calculation
{
    public interface IYieldCalculator
    {
        decimal CalculateFinalValue(decimal initialAmount, int termInMonths);
    }
}
