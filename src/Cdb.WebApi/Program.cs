using Cdb.Domain.Calculation;
using Cdb.Domain.Simulation;
using Cdb.Domain.Tax;
using Cdb.Domain.Validation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<ICdbSimulationService>(_ =>
    new CdbSimulationService(new InvestmentValidator(), new YieldCalculator(), new TaxCalculator()));

var app = builder.Build();

app.MapControllers();

await app.RunAsync();
