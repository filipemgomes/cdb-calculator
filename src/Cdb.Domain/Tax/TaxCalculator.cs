namespace Cdb.Domain.Tax
{
    public class TaxCalculator : ITaxCalculator
    {
        public decimal CalculateTax(decimal finalAmount, decimal initialAmount, int termInMonths)
        {
            var earnings = finalAmount - initialAmount;
            var rate = GetTaxRate(termInMonths);

            return earnings * rate;
        }

        private static decimal GetTaxRate(int termInMonths)
        {
            if (termInMonths <= 6)
            {
                return 0.225m;
            }

            if (termInMonths <= 12)
            {
                return 0.20m;
            }

            if (termInMonths <= 24)
            {
                return 0.175m;
            }

            return 0.15m;
        }
    }
}
