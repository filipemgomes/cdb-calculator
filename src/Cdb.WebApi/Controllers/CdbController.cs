using System.Linq;
using System.Web.Http;
using Cdb.WebApi.Contracts;
using Cdb.Domain.Calculation;
using Cdb.Domain.Simulation;
using Cdb.Domain.Tax;
using Cdb.Domain.Validation;

namespace Cdb.WebApi.Controllers
{
    [RoutePrefix("api/cdb")]
    public class CdbController : ApiController
    {
        private readonly ICdbSimulationService _simulationService;

        public CdbController()
            : this(new CdbSimulationService(new InvestmentValidator(), new YieldCalculator(), new TaxCalculator()))
        {
        }

        public CdbController(ICdbSimulationService simulationService)
        {
            _simulationService = simulationService;
        }

        [HttpPost]
        [Route("simular")]
        public IHttpActionResult Simulate(SimulateRequest request)
        {
            if (request == null || !ModelState.IsValid)
            {
                var modelErrors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => string.IsNullOrEmpty(e.ErrorMessage) ? "Requisição inválida." : e.ErrorMessage)
                    .Distinct()
                    .ToList();

                if (!modelErrors.Any())
                {
                    modelErrors.Add("Requisição inválida.");
                }

                return Content(System.Net.HttpStatusCode.BadRequest, new ValidationErrorResponse
                {
                    Errors = modelErrors
                });
            }

            var result = _simulationService.Simulate(request.InitialValue, request.TermInMonths);

            if (!result.IsValid)
            {
                return Content(System.Net.HttpStatusCode.BadRequest, new ValidationErrorResponse
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
