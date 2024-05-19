using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Net;

namespace QuickOrderPedido.Api.Controllers
{
    public static class HealthController
    {
        public static void RegisterHealthController(this WebApplication app)
        {
            app.MapGet("health", async ([FromServices] HealthCheckService healthCheckService) =>
            {
                HealthReport report = await healthCheckService.CheckHealthAsync();
                var result = new
                {
                    status = report.Status.ToString(),
                    errros = report.Entries.Select(e => new
                    {
                        name = e.Key,
                        status = e.Value.Status.ToString(),
                        description = e.Value.Status != HealthStatus.Healthy ? e.Value.Exception.ToString() : e.Value.Description != null ? e.Value.Description : "Healthcheck Ok!"
                    }).ToList()
                };
                return report.Status == HealthStatus.Healthy ? Results.Ok(result) : Results.Problem(statusCode: (int)HttpStatusCode.ServiceUnavailable);
            });
        }
    }
}
