using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Cdb.WebApi.Contracts;
using Cdb.Domain.Simulation;

namespace Cdb.WebApi.Controllers
{
    [Route("api/cdb")]
    public class CdbController : ControllerBase
    {
        private readonly ICdbSimulationService _simulationService;

        public CdbController(ICdbSimulationService simulationService)
        {
            _simulationService = simulationService;
        }

        [HttpPost("simular")]
        public IActionResult Simulate([FromBody] SimulateRequest request)
        {
            if (request == null || !ModelState.IsValid)
            {
                var modelErrors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => string.IsNullOrEmpty(e.ErrorMessage) ? "Requisição inválida." : e.ErrorMessage)
                    .Distinct()
                    .ToList();

                if (modelErrors.Count == 0)
                {
                    modelErrors.Add("Requisição inválida.");
                }

                return BadRequest(new ValidationErrorResponse
                {
                    Errors = modelErrors
                });
            }

            var result = _simulationService.Simulate(
                request.InitialValue ?? 0m,
                request.TermInMonths ?? 0);

            if (!result.IsValid)
            {
                return BadRequest(new ValidationErrorResponse
                {
                    Errors = result.Errors
                });
            }

            return Ok(new SimulateResponse
            {
                GrossAmount = result.GrossResult,
                NetAmount = result.NetResult
            });
        }
    }
}
