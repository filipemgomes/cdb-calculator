namespace Cdb.Domain.Calculation
{
    public class YieldCalculator : IYieldCalculator
    {
        private static readonly decimal Cdi = 0.009m;
        private static readonly decimal Tb = 1.08m;

        public decimal CalculateFinalValue(decimal initialAmount, int termInMonths)
        {
            var monthlyRate = Cdi * Tb;
            var finalAmount = initialAmount;

            for (var month = 0; month < termInMonths; month++)
            {
                finalAmount *= 1 + monthlyRate;
            }

            return finalAmount;
        }
    }
}
