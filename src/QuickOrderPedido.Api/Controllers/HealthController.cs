using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Net;

namespace QuickOrderPedido.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        private readonly HealthCheckService healthCheckService;

        public HealthController(HealthCheckService healthCheckService)
        {
            this.healthCheckService = healthCheckService;
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = false)]
        public async Task<ActionResult> Get()
        {
            HealthReport report = await this.healthCheckService.CheckHealthAsync();
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
            return report.Status == HealthStatus.Healthy ? this.Ok(result) : this.StatusCode((int)HttpStatusCode.ServiceUnavailable, result);
        }
    }
}
