using App.Metrics.AspNetCore;
using App.Metrics.Formatters.Prometheus;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Identity
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseMetricsWebTracking()
            .UseMetrics(options => {
                options.EndpointOptions = endpointOptions =>
                {
                    endpointOptions.MetricsTextEndpointOutputFormatter = new MetricsPrometheusTextOutputFormatter();
                    endpointOptions.MetricsEndpointOutputFormatter = new MetricsPrometheusProtobufOutputFormatter();
                    endpointOptions.EnvironmentInfoEndpointEnabled = false;

                };
            })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
