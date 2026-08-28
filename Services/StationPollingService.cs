using DBStations.Configuration;
using Microsoft.Extensions.Options;

namespace DBStations.Services
{
    public class StationPollingService(HttpRequester httpRequester, ILogger<StationPollingService> logger, IOptions<ApiSettings> options) : BackgroundService
    {
        private readonly ApiSettings _options = options.Value;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var timer = new PeriodicTimer(TimeSpan.FromDays(1));

            await PollStationDataAsync(stoppingToken); // Initial poll on startup

            while (await timer.WaitForNextTickAsync(stoppingToken))
            {
                await PollStationDataAsync(stoppingToken);
            }
        }

        private async Task PollStationDataAsync(CancellationToken stoppingToken)
        {
            try
            {
                var response = await httpRequester.GetAsync(_options.UrlStations);
                // Process the response as needed
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while polling the station data.");
            }
        }
    }
}
