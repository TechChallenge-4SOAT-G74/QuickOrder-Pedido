using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using QuickOrderPedido.Infra.Gateway.Core;


namespace QuickOrderPedido.IoC
{
    public static class HealthCheckSetupExtentions
    {
        public static void ConfigureHealthCheckEndpoints(this IApplicationBuilder app)
        {
            app.UseHealthChecks("/healthCheck", new HealthCheckOptions()
            {
                Predicate = (_) => false,
                ResultStatusCodes =
                {
                    [HealthStatus.Healthy] = StatusCodes.Status200OK,
                    [HealthStatus.Degraded] = StatusCodes.Status200OK,
                    [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
                }
            });
        }

        public static void ConfigureHealthChecks(
            this IServiceCollection services
            , DatabaseMongoDBSettings configurationMongo)
        {
            services.AddHealthChecks()
                .AddCheck("self", () => HealthCheckResult.Healthy("healthCheck OK!"))
                .AddMongoDb(
                    mongodbConnectionString: configurationMongo.ConnectionString,
                    mongoDatabaseName: configurationMongo.DatabaseName,
                    name: "Mongo - QuickOrderDB",
                    timeout: TimeSpan.FromSeconds(29));


        }
    }
}
