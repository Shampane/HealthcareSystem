using HealthcareSystem.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace HealthcareSystem.Application.Services;

public class ScheduleCleanupService : BackgroundService {
    private readonly ILogger<ScheduleCleanupService> _logger;
    private readonly IServiceProvider _serviceProvider;

    public ScheduleCleanupService(ILogger<ScheduleCleanupService> logger,
        IServiceProvider serviceProvider) {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken ct) {
        _logger.LogInformation("Schedule cleanup service is starting.");
        while (!ct.IsCancellationRequested) {
            try {
                await using (
                    AsyncServiceScope scope =
                    _serviceProvider.CreateAsyncScope()
                ) {
                    var repository = scope.ServiceProvider
                        .GetRequiredService<IScheduleRepository>();

                    int oldCount = await repository.GetSchedulesCount();
                    await repository.ClearOldSchedulesAsync();
                    int newCount = await repository.GetSchedulesCount();
                    int cleanedCount = oldCount - newCount;

                    _logger.LogInformation(
                        $"Cleaned {cleanedCount} old schedules.");
                }

                int hourSpan = 24 - DateTime.UtcNow.Hour;
                int numberOfHours = hourSpan;
                if (hourSpan == 24) {
                    numberOfHours = 24;
                }

                await Task.Delay(TimeSpan.FromHours(numberOfHours), ct);
            }
            catch (TaskCanceledException) {
                _logger.LogInformation(
                    "Schedule cleanup service is canceled."
                );
            }
            catch (Exception ex) {
                _logger.LogInformation($"Error: {ex.Message}");
            }
        }

        _logger.LogInformation("Schedule cleanup service is stopping.");
    }
}