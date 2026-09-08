namespace Cdb.Domain.Simulation
{
    public interface ICdbSimulationService
    {
        CdbSimulationResult Simulate(decimal initialAmount, int termInMonths);
    }
}
