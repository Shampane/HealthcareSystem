using HealthcareSystem.Infrastructure.Schedules;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace HealthcareSystem.Application.Schedules;

public class ScheduleCleanupService(
    ILogger<ScheduleCleanupService> logger,
    IServiceProvider serviceProvider
) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken ct)
    {
        logger.LogInformation("Schedule cleanup service is starting.");
        while (!ct.IsCancellationRequested)
            try
            {
                await using (
                    var scope = serviceProvider.CreateAsyncScope()
                )
                {
                    var repository = scope.ServiceProvider
                        .GetRequiredService<IScheduleRepository>();

                    var oldCount = await repository.GetSchedulesCount();
                    await repository.ClearOldSchedulesAsync();
                    var newCount = await repository.GetSchedulesCount();
                    var cleanedCount = oldCount - newCount;

                    logger.LogInformation(
                        $"Cleaned {cleanedCount} old schedules.");
                }

                var hourSpan = 24 - DateTime.UtcNow.Hour;
                var numberOfHours = hourSpan;
                if (hourSpan == 24)
                    numberOfHours = 24;

                await Task.Delay(TimeSpan.FromHours(numberOfHours), ct);
            }
            catch (TaskCanceledException)
            {
                logger.LogInformation(
                    "Schedule cleanup service is canceled."
                );
            }
            catch (Exception ex)
            {
                logger.LogInformation($"Error: {ex.Message}");
            }

        logger.LogInformation("Schedule cleanup service is stopping.");
    }
}