using DBStations.Configuration;
using Microsoft.Extensions.Options;

namespace DBStations.Services
{
    public class FacilityPollingService(HttpRequester httpRequester, ILogger<FacilityPollingService> logger, IOptions<ApiSettings> options) : BackgroundService
    {
        private readonly ApiSettings _options = options.Value;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var timer = new PeriodicTimer(TimeSpan.FromDays(1));

            await PollFacilityDataAsync(stoppingToken); // Initial poll on startup

            while (await timer.WaitForNextTickAsync(stoppingToken))
            {
                await PollFacilityDataAsync(stoppingToken);
            }
        }

        private async Task PollFacilityDataAsync(CancellationToken stoppingToken)
        {
            try
            {
                var response = await httpRequester.GetAsync(_options.UrlFacilities);
                // Process the response as needed
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while polling the facility data.");
            }
        }
    }
}
