namespace Cdb.Domain.Tax
{
    public interface ITaxCalculator
    {
        decimal CalculateTax(decimal finalAmount, decimal initialAmount, int termInMonths);
    }
}
