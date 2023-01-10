using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using task_tracker.Controllers;
using task_tracker.Interfaces;

namespace task_tracker
{
    public class HubTimer : BackgroundService
    {
        private readonly ILogger<HubTimer> _logger;
        private readonly IConfiguration Configuration;
        private readonly IServiceProvider _serviceProvider;

        public HubTimer(ILogger<HubTimer> logger, IServiceProvider serviceProvider, IConfiguration configuration)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            Configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation(
            $"{nameof(HubTimer)} is running.");

            await DoWorkAsync(stoppingToken);
        }

        private async Task DoWorkAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation(
                $"{nameof(HubTimer)} is working.");

            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                ISuccessesFacade _successesFacade = scope.ServiceProvider.GetRequiredService<ISuccessesFacade>();
                IUsersFacade _usersFacade = scope.ServiceProvider.GetRequiredService<IUsersFacade>();

                while (!stoppingToken.IsCancellationRequested)
                {
                    try
                    {
                        _logger.LogInformation("HubTimer is running at: {time}", DateTimeOffset.Now);
                        await _successesFacade.GetAllSuccessesWithHub();
                        await _usersFacade.GetAllUsersWithHub();
                    }

                    catch (Exception ex)
                    {
                        _logger.LogError("Error while starting HubTimer. Exception: {@Exception}", ex.ToString());
                    }

                    await Task.Delay(int.Parse(Configuration["ServiceConfiguration:Interval"]), stoppingToken);
                }
            }
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation(
                $"{nameof(HubTimer)} is stopping.");

            await base.StopAsync(stoppingToken);
        }
    }
}
