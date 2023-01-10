using task_tracker.Interfaces;

namespace task_tracker
{
    public class HubTimerSuccess : BackgroundService
    {
        private readonly ILogger<HubTimerSuccess> _logger;
        private readonly IConfiguration Configuration;
        private readonly IServiceProvider _serviceProvider;

        public HubTimerSuccess(ILogger<HubTimerSuccess> logger, IServiceProvider serviceProvider, IConfiguration configuration)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            Configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation(
            $"{nameof(HubTimerSuccess)} is running.");

            await DoWorkAsync(stoppingToken);
        }

        private async Task DoWorkAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation(
                $"{nameof(HubTimerSuccess)} is working.");

            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                ISuccessesFacade _successesFacade = scope.ServiceProvider.GetRequiredService<ISuccessesFacade>();

                while (!stoppingToken.IsCancellationRequested)
                {
                    try
                    {
                        _logger.LogInformation("SUCCESS HubTimer is running at: {time}", DateTimeOffset.Now);
                        await _successesFacade.GetAllSuccessesWithHub();
                    }

                    catch (Exception ex)
                    {
                        _logger.LogError("Error while starting SUCCESS HubTimer. Exception: {@Exception}", ex.ToString());
                    }

                    await Task.Delay(int.Parse(Configuration["ServiceConfiguration:Interval"]), stoppingToken);
                }
            }
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation(
                $"{nameof(HubTimerSuccess)} is stopping.");

            await base.StopAsync(stoppingToken);
        }
    }
}
