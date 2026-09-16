using Azure.Monitor.OpenTelemetry.AspNetCore;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace SFA.DAS.EmployerFeedback.Web.StartupExtensions
{
    [ExcludeFromCodeCoverage]
    public static class AddOpenTelemetryExtensions
    {
        /// <summary>
        /// Add the OpenTelemetry telemetry service to the application.
        /// </summary>
        /// <param name="services">Service Collection</param>
        /// <param name="appInsightsConnectionString">Azure app insights connection string.</param>
        public static void AddOpenTelemetryRegistration(this IServiceCollection services, string appInsightsConnectionString)
        {
            if (!string.IsNullOrEmpty(appInsightsConnectionString))
            {
                services.AddApplicationInsightsTelemetry();
                // This service will collect and send telemetry data to Azure Monitor.
                services.AddOpenTelemetry().UseAzureMonitor(options =>
                {
                    options.ConnectionString = appInsightsConnectionString;
                });
            }
            else
            {
                services.AddSingleton(TelemetryConfiguration.CreateDefault());
                services.AddSingleton<Microsoft.ApplicationInsights.AspNetCore.JavaScriptSnippet>();
            }
        }
    }
}