using task_tracker.Interfaces;

namespace task_tracker
{
    public class HubTimerUser : BackgroundService
    {
        private readonly ILogger<HubTimerUser> _logger;
        private readonly IConfiguration Configuration;
        private readonly IServiceProvider _serviceProvider;

        public HubTimerUser(ILogger<HubTimerUser> logger, IServiceProvider serviceProvider, IConfiguration configuration)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            Configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation(
            $"{nameof(HubTimerUser)} is running.");

            await DoWorkAsync(stoppingToken);
        }

        private async Task DoWorkAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation(
                $"{nameof(HubTimerUser)} is working.");

            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                IUsersFacade _usersFacade = scope.ServiceProvider.GetRequiredService<IUsersFacade>();

                while (!stoppingToken.IsCancellationRequested)
                {
                    try
                    {
                        _logger.LogInformation("USER HubTimer is running at: {time}", DateTimeOffset.Now);
                        await _usersFacade.GetAllUsersWithHub();
                    }

                    catch (Exception ex)
                    {
                        _logger.LogError("Error while starting USER HubTimer. Exception: {@Exception}", ex.ToString());
                    }

                    await Task.Delay(int.Parse(Configuration["ServiceConfiguration:Interval"]), stoppingToken);
                }
            }
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation(
                $"{nameof(HubTimerUser)} is stopping.");

            await base.StopAsync(stoppingToken);
        }
    }
}
